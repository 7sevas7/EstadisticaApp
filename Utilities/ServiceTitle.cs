


namespace EstadisticaApp.Utilities
{
    public class ServiceTitle : EventArgs
    {
        public string State { set; get; } = "Main";
        public List<string> TextUnidad { get; private set; } = new List<string> {
            "Sistemas DIF",
            "Junta de Asistencia",
            "Hospital de Niño DIF",
            "CRIH",
            "Procuraduría"
        };

    }
}
