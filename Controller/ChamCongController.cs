using ChamCongAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace ChamCongAPI.Controllers
{
    using NhanVienModel = ChamCongAPI.Models.NhanVien;
    using LichSuChamCongModel = ChamCongAPI.Models.LichSuChamCong;

    public class ChamCongController : Controller
    {
        private readonly AppDbContext _context;

        public ChamCongController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LichSu()
        {
            var list = _context.LichSuChamCongs
                        .Include(l => l.NhanVien)
                        .OrderByDescending(l => l.Ngay)
                        .ToList();

            foreach (var item in list)
            {
                if (item.NhanVien != null)
                {
                    item.HoTen = item.NhanVien.HoTen;
                }
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult QuetMat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> QuetMat(IFormFile AnhQuet)
        {
            if (AnhQuet == null)
            {
                ViewBag.Error = "Chưa chọn ảnh";
                return View();
            }

            List<float>? inputEmbedding = null;
try
{
    inputEmbedding = await GetEmbeddingFromAI(AnhQuet);
}
catch(Exception ex)
{
    ViewBag.Error = "Lỗi server khi gọi AI: " + ex.Message;
    return View();
}


            NhanVienModel? bestMatch = null;
            double bestScore = 0;

            foreach (var nv in _context.NhanViens)
            {
                if (string.IsNullOrEmpty(nv.FaceEmbedding)) continue;

                var dbEmbedding = JsonSerializer.Deserialize<List<float>>(nv.FaceEmbedding);
                if (dbEmbedding == null) continue;

                var score = CosineSimilarity(inputEmbedding, dbEmbedding);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMatch = nv;
                }
            }

            if (bestMatch == null || bestScore < 0.6)
            {
                ViewBag.Error = "Không tìm thấy nhân viên phù hợp";
                return View();
            }

            ViewBag.ThanhCong = true;
            ViewBag.NhanVien = bestMatch;
            ViewBag.Score = bestScore;

            return View();
        }

        private double CosineSimilarity(List<float> v1, List<float> v2)
        {
            double dot = 0, mag1 = 0, mag2 = 0;
            for (int i = 0; i < v1.Count; i++)
            {
                dot += v1[i] * v2[i];
                mag1 += v1[i] * v1[i];
                mag2 += v2[i] * v2[i];
            }
            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }

        private async Task<List<float>?> GetEmbeddingFromAI(IFormFile file)
        {
            using var client = new HttpClient();
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

            var response = await client.PostAsync("http://localhost:8000/embedding", content);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.GetProperty("success").GetBoolean()) return null;

            return doc.RootElement
                .GetProperty("embedding")
                .EnumerateArray()
                .Select(x => x.GetSingle())
                .ToList();
        }
    }
}
