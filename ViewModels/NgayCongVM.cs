namespace WebApp.ViewModels
{
    public class NgayCongVM
    {
        public string CaLam { get; set; } = "HC";
        public string GioVao { get; set; } = "--:--";
        public string GioRa { get; set; } = "--:--";

        // green | yellow | red | gray
        public string Mau { get; set; } = "gray";
    }
}
