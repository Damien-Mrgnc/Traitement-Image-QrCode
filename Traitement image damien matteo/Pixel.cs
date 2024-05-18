namespace Image_Morganico_Dupin
{
    public class Pixel
    {
        private byte rouge { get; }
        private byte vert { get; }
        private byte bleu { get; }
        public Pixel()
        {
            this.rouge = 0;
            this.vert = 0;
            this.bleu = 0;
        }
        public Pixel(byte bleu, byte vert, byte rouge)
        {
            this.rouge = rouge;
            this.vert = vert;
            this.bleu = bleu;
        }
        public byte Bleu { get { return this.bleu; } set { value = bleu; } }
        public byte Vert { get { return this.vert; } set { value = rouge; } }
        public byte Rouge
        {
            get { return this.rouge; }
            set { value = rouge; }
        }
        public string ToString()
        {
            return "Rouge : " + rouge + " Vert : " + vert + " Rouge : " + rouge;
        }
        public bool Equals(Pixel other)
        {
            return (other.Bleu == bleu && other.Rouge == rouge && other.Vert == vert);
        }
    }
}
