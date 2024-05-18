namespace Image_Morganico_Dupin
{
    public class Imaginaire
    {
        private double reel;
        private double i;

        public Imaginaire(double reel, double imaginaire)
        {
            this.reel = reel;
            this.i = imaginaire;
        }

        public double Reel { get { return reel; } }
        public double I { get { return i; } }
        /// <summary>
        /// calcule le module carré d'un nombre complexe
        /// </summary>
        /// <returns>le module</returns>
        public double ModuleCarre()
        {
            return Math.Sqrt(reel * reel + i * i);
        }

        public string Affichage()
        {
            return $"{reel} + {i}i";
        }
        /// <summary>
        /// fais la multiplication de deux nombre complexe en calculant d'une part la parti re=éel du resultat et d'une autre part la parti imaginaire
        /// </summary>
        /// <param name="a">le complexe a multiplier</param>
        /// <returns>le resultat de la multiplication</returns>
        public Imaginaire Multiplication(Imaginaire a)
        {
            double reelResultat = a.Reel * Reel - a.I * I;
            double imaginaireResultat = a.Reel * I + a.I * Reel;
            return new Imaginaire(reelResultat, imaginaireResultat);
        }
        /// <summary>
        /// fais l'addition de deux nombre complexe en calculant d'une part la parti réel du resultat et d'une autre part la parti imaginaire
        /// </summary>
        /// <param name="a">le complexe a additionner</param>
        /// <returns>le resltat de l'addition</returns>
        public Imaginaire Addition(Imaginaire a)
        {
            double reelResultat = a.Reel + Reel;
            double imaginaireResultat = a.I + I;
            return new Imaginaire(reelResultat, imaginaireResultat);
        }
    }

}
