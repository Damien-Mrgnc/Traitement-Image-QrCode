using Image_Morganico_Dupin;

namespace Image_Morganico_Dupin
{
    public class QRCode
    {
        private string message;
        private MyImage QrCode;
        public QRCode()
        {
            string message = "";
            QrCode = null;
        }
       
        public QRCode(string message)
        {
            this.message = message;
            this.QrCode = new MyImage();
            byte version = 0;
            int taillemax = 0;
            if (message.Length < 26)
            {
                version = 1;
                taillemax = 21;
            }
            else if (message.Length > 25 && message.Length < 46)
            {
                version = 2;
                taillemax = 21+(version-1)*4;
            }
            else if (message.Length > 45 && message.Length < 78)
            {
                version = 3;
                taillemax = 21 + (version - 1) * 4;
            }
            else if (message.Length > 77 && message.Length < 115)
            {
                version = 4;
                taillemax = 21 + (version - 1) * 4;
            }
            else if (message.Length > 114 && message.Length < 155)
            {
                version = 5;
                taillemax = 21 + (version - 1) * 4;
            }
            CreationBase(taillemax, TableauQRCODE(message.ToUpper(), version), version);
            QrCode.Agrandir(16);
            QrCode.From_Image_To_File("QRCode.bmp");
        }
        public MyImage QRCODE { get { return QrCode; } set { QrCode = value; } }
        public void Decode()
        {
            Console.WriteLine("Veuillez entrer le nom du fichier : ");
            string name = Console.ReadLine();
            name += ".bmp";
            try
            { 
                MyImage decode = new MyImage(name);
                while (decode.Offset==0)
                {
                    Console.Clear();
                    Console.WriteLine("Veuillez entrer le nom du fichier : ");
                    name = Console.ReadLine();
                    name += ".bmp";
                    decode = new MyImage(name);
                }
                Pixel blanc = new Pixel(255, 255, 255);
                int i = 0;
                int j = 0;
                bool i1= false;
                bool i2= false;
                while (!i2)
                {
                    if (decode.Matrice[i, i].Bleu ==255)
                    {
                        break;
                    }
                    
                    i++;
                }
                decode.Retrecir(i);
                int version = (decode.Matrice.GetLength(0)-21)/4;
                DeCodageQrCode(decode, version+1);
            }
            catch
            {
                Decode();
            }
        }

        /// <summary>
        /// Crée une matrice QR code en fonction des paramètres fournis.
        /// </summary>
        /// <param name="taille">Taille de la matrice QR code.</param>
        /// <param name="tabl">Tableau d'octets représentant les données à encoder.</param>
        /// <param name="version">Version du QR code.</param>
        public void CreationBase(int taille, byte[] tabl, int version)
        {
            Pixel[,] mat = new Pixel[taille, taille];
            int[] tab = { 0, 0, 0, taille - 7, taille - 7, 0 };
            for (int place = 0; place < tab.Length; place += 2)
            {
                int n = 0;
                for (int k = 0; k < 4; k++)
                {
                    for (int i = k - 1 + tab[place]; i < 8 - k + tab[place]; i++)
                    {
                        for (int j = k - 1 + tab[place + 1]; j < 8 - k + tab[place + 1]; j++)
                        {
                            if (i >= 0 && j >= 0 && i < taille && j < taille)
                            {
                                if (k % 2 == 0) mat[i, j] = new Pixel(255, 255, 255);
                                else if (k % 2 == 1) mat[i, j] = new Pixel(0, 0, 0);//b
                            }
                        }
                    }
                }
            }
            if (version > 1)
            {
                for (int k = 0; k < 3; k++)
                {
                    byte couleur = (byte)((k % 2 == 0) ? 0 : 255);
                    for (int i = k + taille - 9; i < taille - 4 - k; i++)
                    {
                        for (int j = k + taille - 9; j < taille - 4 - k; j++)
                        {
                            mat[i, j] = new Pixel(couleur, couleur, couleur);
                        }
                    }
                }
            }
            SousMasque(mat);
            Pointille(mat);
            Console.WriteLine();
            Remplissage(mat, tabl);
            AfficherMatricePixel(mat);

            QrCode.Width = taille;
            QrCode.Height = taille;
            QrCode.TailleFichier = QrCode.Offset + taille * taille * 3;
            QrCode.Matrice = mat;
        }

        /// <summary>
        /// Génère un tableau de bytes représentant le code QR à partir de la chaîne de caractères spécifiée et de la version du QR code.
        /// </summary>
        /// <param name="chaine">Chaîne de caractères à encoder en QR code.</param>
        /// <param name="version">Version du QR code à utiliser pour l'encodage.</param>
        /// <returns>Tableau de bytes représentant le code QR généré.</returns>
        public byte[] TableauQRCODE(string chaine, byte version)
        {
            int Codage = -1;
            int Codage1 = -1;
            Console.WriteLine("Version : "+version);
            if (version == 1)
            {
                Codage = 26 * 8;
                Codage1 = 19 * 8;
            }
            if (version == 2)
            {
                Codage = 44 * 8;
                Codage1 = 34 * 8;
            }
            if (version == 3)
            {
                Codage = 70 * 8;
                Codage1 = 55 * 8;
            }
            if (version == 4)
            {
                Codage = 100 * 8;
                Codage1 = 80 * 8;
            }
            if (version == 5)
            {
                Codage = (108+26) * 8;
                Codage1 = 108 * 8;
            }
            byte[] tableau = new byte[Codage];
            tableau[0] = 0;
            tableau[1] = 0;
            tableau[2] = 1;
            tableau[3] = 0;
            byte[] nb_caractere = DecimalToBinary(chaine.Length, 9);
            for (int i = 0; i < 9; i++)
            {
                tableau[i + 4] = nb_caractere[i];
            }
            int index = 13;
            char a = ' ';
            char b = ' ';
            bool etat = false;
            for (int i = 0; i < chaine.Length; i += 2)
            {
                int n = 0;
                a = chaine[i];
                if (chaine.Length == i + 1)
                {
                    b = chaine[i];
                    etat = true;
                    n += 5;
                }
                else b = chaine[i + 1];
                char[] tableauDeCaracteres = {
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',' ',
                    '$', '%', '*', '+', '-', '.', '/', ':'};
                byte[] binaire = CharToByteTab(a, b, etat, tableauDeCaracteres);
                int l = 0;
                if (etat) l += 5;
                for (int j = 0; j < binaire.Length - l; j++)
                {
                    tableau[j + index] = binaire[j + l];
                }
                index += binaire.Length - l;
            }
            tableau[index] = 0;
            tableau[index + 1] = 0;
            tableau[index + 2] = 0;
            tableau[index + 3] = 0;
            index += 4;
            while (index % 8 != 0)
            {
                tableau[index] = 0;
                index++;
            }
            int m = 0;
            while (index < Codage1)
            {
                if (tableau[index] == 0)
                {
                    byte[] k = new byte[11];
                    if (m % 2 == 0) { k = DecimalToBinary(236, 11); }
                    else { k = DecimalToBinary(17, 11); }
                    for (int j = 0; j < k.Length - 3; j++)
                    {
                        tableau[j + index] = k[j + 3];
                    }
                    index += 8;
                    m++;
                }
            }
            byte[] test = new byte[Codage1];
            for (int i = 0; i < Codage1; i++)
            {
                test[i] = tableau[i];
            }
            byte[] bytesa = BINAIRETOBYTE(test);
            byte[] result = ReedSolomonAlgorithm.Encode(bytesa, (Codage - Codage1) / 8, ErrorCorrectionCodeType.QRCode);
            byte[] ReedSolomon = ReedSolomonCreate(result);
            for (int i = 0; i < ReedSolomon.Length; i++)
            {
                tableau[i + index] = ReedSolomon[i];
            }
            return tableau;
        }

        /// <summary>
        /// Convertit deux caractères en un tableau binaire de byte en utilisant une table de caractères spécifiée.
        /// </summary>
        /// <param name="a">Premier caractère à convertir.</param>
        /// <param name="b">Deuxième caractère à convertir.</param>
        /// <param name="etat">Indique si le deuxième caractère est le même que le premier (état de répétition).</param>
        /// <param name="tableauDeCaracteres">Tableau de caractères utilisé pour la conversion.</param>
        /// <returns>Tableau de byte représentant la conversion des caractères en binaire.</returns>
        public byte[] CharToByteTab(char a, char b, bool etat, char[] tableauDeCaracteres)
        {

            int i1 = recherche_tabchar(tableauDeCaracteres, char.ToUpper(a));
            int i2 = recherche_tabchar(tableauDeCaracteres, char.ToUpper(b));
            if (etat) i1 = 0;
            int finalIndex = 45 * i1 + i2;
            return DecimalToBinary(finalIndex, 11);
        }

        /// <summary>
        /// Convertit un tableau binaire de byte en un tableau de byte.
        /// </summary>
        /// <param name="tab">Tableau binaire de byte à convertir.</param>
        /// <returns>Tableau de byte résultant de la conversion.</returns>
        public byte[] BINAIRETOBYTE(byte[] tab)
        {
            byte[] final = new byte[tab.Length / 8];
            int index = 0;
            for (int i = 0; i < tab.Length; i += 8)
            {
                byte[] cas = new byte[8];
                for(int j = 0; j<8; j++)
                {
                    cas[j] = tab[i + j];
                        
                }
                byte b = bytetobyte(cas);
                final[index] = (byte)b;
                index++;
            }
            return final;
        }

        /// <summary>
        /// Convertit un tableau binaire d'octets en un octet.
        /// </summary>
        /// <param name="tab">Tableau binaire d'octets à convertir.</param>
        /// <returns>Octet résultant de la conversion.</returns>
        public byte bytetobyte(byte[] tab)
        {
            byte b = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                b += (byte)(tab[tab.Length - 1 - i] * Math.Pow(2, i));
            }
            return b;
        }

        /// <summary>
        /// Recherche récursive dichotomique dans un tableau de caractères pour trouver une valeur spécifique.
        /// </summary>
        /// <param name="tab">Tableau de caractères dans lequel effectuer la recherche.</param>
        /// <param name="val">Valeur à rechercher dans le tableau.</param>
        /// <param name="g">Indice de début de la recherche dans le tableau.</param>
        /// <param name="d">Indice de fin de la recherche dans le tableau.</param>
        /// <returns>Indice de la valeur recherchée dans le tableau.</returns>
        public int recherche_tabchar(char[] tab, char val)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == val)
                {
                    return i;
                }
            }
            return 36;
        }

        /// <summary>
        /// Convertit un nombre décimal en binaire sous forme d'un tableau d'octets.
        /// </summary>
        /// <param name="Nbr">Nombre décimal à convertir en binaire.</param>
        /// <param name="taille">Taille du tableau binaire en bits.</param>
        /// <returns>Tableau d'octets représentant la conversion binaire du nombre.</returns>
        public byte[] DecimalToBinary(int Nbr, int taille)
        {
            byte[] retour = new byte[taille];

            for (int i = retour.Length - 1; i >= 0; i--)
            {
                retour[i] = (byte)(Nbr % 2);
                Nbr /= 2;
            }
            return retour;
        }

        /// <summary>
        /// Crée une représentation de correction d'erreur Reed-Solomon à partir d'un tableau d'octets spécifié.
        /// </summary>
        /// <param name="tab">Tableau d'octets à utiliser pour la création de la correction d'erreur.</param>
        /// <returns>Tableau d'octets représentant la correction d'erreur Reed-Solomon.</returns>
        public byte[] ReedSolomonCreate(byte[] tab)
        {
            byte[] result = new byte[tab.Length * 8];
            for (int i = 0; i < tab.Length; i++)
            {
                byte[] e = DecimalToBinary(tab[i], 11);
                for (int k = 0; k < 8; k++)
                {
                    result[i * 8 + k] = e[k + 3];
                }
            }
            return result;
        }

        /// <summary>
        /// Décodage du contenu d'un QR code à partir d'une image spécifiée.
        /// </summary>
        /// <param name="QrCode">Image contenant le QR code à décoder.</param>
        /// <param name="version">Version du QR code à prendre en compte pour le décodage.</param>
        public void DeCodageQrCode(MyImage QrCode, int version)
        {
            Pixel blanc = new Pixel(255, 255, 255);
            Pixel[,] mat = QrCode.Matrice;
            Pixel test = blanc;

            int Codage = -1;
            if (version == 1)
            {
                Codage = 26 * 8;
            }
            if (version == 2)
            {
                Codage = 44 * 8;
            }
            if (version == 3)
            {
                Codage = 70 * 8;
            }
            if (version == 4)
            {
                Codage = 100 * 8;
            }
            if (version == 5)
            {
                Codage = 134 * 8;
            }
            while (test.Bleu != 0)
            {
                test = mat[mat.GetLength(0) - 2, mat.GetLength(0) - 2];
                if (test.Bleu == 255)
                    QrCode.Rotation90();
                mat = QrCode.Matrice;
            }

            AppliquerZonesMasquage(mat, version);
            ConvertirMatriceEnBinaire(mat, Codage);
        }

        /// <summary>
        /// Applique les zones de masquage sur une matrice de pixels en fonction de la version du QR code.
        /// </summary>
        /// <param name="mat">Matrice de pixels représentant le QR code.</param>
        /// <param name="version">Version du QR code à considérer pour l'application des zones de masquage.</param>
        public void AppliquerZonesMasquage(Pixel[,] mat, int version)
        {
            int taille = mat.GetLength(0);
            Pixel vide = new Pixel(100, 0, 0);
            int[] tab = { 0, 0, 0, taille - 7, taille - 7, 0 };

            for (int place = 0; place < tab.Length; place += 2)
            {
                for (int k = 0; k < 4; k++)
                {
                    for (int i = k - 1 + tab[place]; i < 8 - k + tab[place]; i++)
                    {
                        for (int j = k - 1 + tab[place + 1]; j < 8 - k + tab[place + 1]; j++)
                        {
                            if (i >= 0 && j >= 0 && i < taille && j < taille)
                            {
                                if (k % 2 == 0)
                                    mat[i, j] = vide;
                                else if (k % 2 == 1)
                                    mat[i, j] = vide;
                            }
                        }
                    }
                }
            }
            if (version > 1)
            {
                for (int k = 0; k < 3; k++)
                {
                    byte couleur = (byte)((k % 2 == 0) ? 0 : 255);
                    for (int i = k + taille - 9; i < taille - 4 - k; i++)
                    {
                        for (int j = k + taille - 9; j < taille - 4 - k; j++)
                        {
                            mat[i, j] = vide;
                        }
                    }
                }
            }

            int ik = 6;
            for (int i = 8; i < mat.GetLength(1) - 8; i++)
            {
                mat[ik, i] = vide;
                mat[i, ik] = vide;
            }
            for (int i = 0; i < mat.GetLength(1); i++)
            {
                if (i < 9 || i > mat.GetLength(0) - 9)
                    mat[8, i] = vide;
            }
            for (int j = mat.GetLength(0) - 1; j >= 0; j--)
            {
                if (j < 9 || j > mat.GetLength(0) - 8)
                    mat[j, 8] = vide;
            }
            mat[mat.GetLength(0) - 8, 8] = vide;
        }

        /// <summary>
        /// Convertit une matrice de pixels en une séquence binaire en suivant un chemin spécifique.
        /// </summary>
        /// <param name="mat">Matrice de pixels représentant le QR code.</param>
        /// <param name="t">Taille de la séquence binaire souhaitée.</param>
        public void ConvertirMatriceEnBinaire(Pixel[,] mat, int t)
        {
            byte[] tab1 = new byte[t];
            byte un = 1;
            byte zero = 0;
            int m = mat.GetLength(0) - 1;
            int ni = mat.GetLength(1) - 1;
            int index = 0;
            bool monte = true;

            while (ni > 0)
            {
                if (monte)
                {
                    while (m >= 0)
                    {
                        if (mat[m, ni].Bleu != 100)
                        {
                            if (index < tab1.Length)
                            {
                                SurMasqueDeCode(m, ni, mat);
                                tab1[index] = mat[m, ni].Bleu == 0 ? un : zero;
                                index++;
                            }
                        }
                        if (mat[m, ni - 1].Bleu != 100)
                        {
                            if (index < tab1.Length)
                            {
                                SurMasqueDeCode(m, ni - 1, mat);
                                tab1[index] = mat[m, ni - 1].Bleu == 0 ? un : zero;
                                index++;
                            }
                        }
                        m--;
                    }
                    m++;
                }
                else
                {
                    while (m < mat.GetLength(0))
                    {
                        if (mat[m, ni].Bleu != 100)
                        {
                            if (index < tab1.Length)
                            {
                                SurMasqueDeCode(m, ni, mat);
                                tab1[index] = mat[m, ni].Bleu == 0 ? un : zero;
                                index++;
                            }
                        }
                        if (mat[m, ni - 1] != null)
                        {
                            if (mat[m, ni - 1].Bleu != 100)
                            {
                                if (index < tab1.Length)
                                {
                                    SurMasqueDeCode(m, ni - 1, mat);
                                    tab1[index] = mat[m, ni - 1].Bleu == 0 ? un : zero;
                                    index++;
                                }
                            }
                            m++;
                        }
                    }
                    m--;
                }
                monte = !monte;
                ni -= 2;
                if (ni > 0)
                {
                    if (mat[9, ni].Bleu == 100)
                        ni--;
                }
            }
            Console.WriteLine();
            byte[] taillebinaire = new byte[9];
            for (int i = 0; i < 9; i++)
            {
                taillebinaire[i] = tab1[i + 4];
            }
            byte taille = bytetobyte(taillebinaire);
            DeCodeDonnees(tab1, taille);
        }

        /// <summary>
        /// Décodage des données à partir d'une séquence binaire en utilisant une taille spécifiée.
        /// </summary>
        /// <param name="tab">Tableau d'octets représentant la séquence binaire à décoder.</param>
        /// <param name="taille">Taille de la séquence binaire utilisée pour le décodage.</param>
        public void DeCodeDonnees(byte[] tab, byte taille)
        {
            int t = 0;
            bool etat = false;
            byte[] donnees = null;
            if (taille % 2 == 1)
            {
                t = taille - 1;
                etat = true;
                donnees = new byte[(t / 2) * 11 + 6];
            }
            else
            {
                t = taille;
                donnees = new byte[(t / 2) * 11];
            }
            for (int j = 0; j < donnees.Length; j++)
            {
                donnees[j] = tab[j + 13];
            }
            byte[] RS = new byte[tab.Length - donnees.Length - 13];
            for (int k = 0; k < RS.Length; k++)
            {
                RS[k] = tab[k + donnees.Length + 13];
            }

            char[] tableauDeCaracteres = {
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',' ',
                    '$', '%', '*', '+', '-', '.', '/', ':'};
            for (int m = 0; m < donnees.Length; m += 11)
            {
                if (m + 11 < donnees.Length || !etat)
                {
                    byte[] lettre = new byte[11];
                    for (int n = 0; n < 11; n++)
                    {
                        lettre[n] = donnees[m + n];
                    }
                    (char lettre1, char lettre2) = BytetabToChar(lettre, tableauDeCaracteres);
                    Console.Write(lettre1 + "" + lettre2);
                }
                else
                {
                    byte[] lettrebinaire = new byte[6];
                    for (int i = 0; i < 6; i++)
                    {
                        lettrebinaire[i] = donnees[m + i];

                    }
                    int val = BinaryToDecimal(lettrebinaire);
                    Console.Write(tableauDeCaracteres[val]);
                }
            }

        }

        /// <summary>
        /// Convertit une séquence binaire de 11 bits en deux caractères en utilisant un tableau de caractères spécifié.
        /// </summary>
        /// <param name="base11">Séquence binaire de 11 bits à convertir.</param>
        /// <param name="tableauDeCaracteres">Tableau de caractères utilisé pour la conversion.</param>
        /// <returns>Un tuple contenant deux caractères correspondant à la conversion de la séquence binaire.</returns>
        public (char, char) BytetabToChar(byte[] base11, char[] tableauDeCaracteres)
        {
            int decimalValue = BinaryToDecimal(base11);

            int i1 = decimalValue / 45;
            int i2 = decimalValue % 45;

            char char1 = tableauDeCaracteres[i1];

            char char2 = tableauDeCaracteres[i2];

            return (char1, char2);
        }

        /// <summary>
        /// Convertit une séquence binaire représentée par un tableau d'octets en sa valeur décimale équivalente.
        /// </summary>
        /// <param name="binary">Tableau d'octets représentant la séquence binaire.</param>
        /// <returns>Valeur décimale correspondante à la séquence binaire.</returns>
        public int BinaryToDecimal(byte[] binary)
        {
            int decimalValue = 0;
            for (int i = 0; i < binary.Length; i++)
            {
                decimalValue += binary[i] * (int)Math.Pow(2, binary.Length - 1 - i);
            }
            return decimalValue;
        }

        /// <summary>
        /// Applique un masquage spécifique sur un pixel de la matrice.
        /// </summary>
        /// <param name="i">Position en ligne du pixel dans la matrice.</param>
        /// <param name="j">Position en colonne du pixel dans la matrice.</param>
        /// <param name="mat">Matrice de pixels où le masquage doit être appliqué.</param>
        public void SurMasqueDeCode(int i, int j, Pixel[,] mat)
        {
            Pixel blanc = new Pixel(255, 255, 255);
            Pixel noir = new Pixel(0, 0, 0);
            if ((i + j) % 2 == 0)
            {
                mat[i, j] = mat[i, j].Equals(noir) ? blanc : noir;
            }
        }

        /// <summary>
        /// Applique un motif en pointillé sur une partie spécifique de la matrice.
        /// </summary>
        /// <param name="mat">Matrice de pixels où le motif en pointillé doit être appliqué.</param>
        public void Pointille(Pixel[,] mat)
        {
            int k = 6;
            for (int i = 8; i < mat.GetLength(1) - 8; i++)
            {
                if (i % 2 == 0)
                {
                    mat[k, i] = new Pixel(0, 0, 0);
                    mat[i, k] = new Pixel(0, 0, 0);
                }
                else
                {
                    mat[k, i] = new Pixel(255, 255, 255);
                    mat[i, k] = new Pixel(255, 255, 255);
                }
            }
        }

        /// <summary>
        /// Applique un sous-masquage spécifique sur une partie de la matrice.
        /// </summary>
        /// <param name="mat">Matrice de pixels où le sous-masquage doit être appliqué.</param>
        public void SousMasque(Pixel[,] mat)
        {

            int[] tab = { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0 };
            Pixel blanc = new Pixel(255, 255, 255);
            Pixel noir = new Pixel(0, 0, 0);
            int n = 0;
            for (int i = 0; i < mat.GetLength(1); i++)
            {
                if (i == 6) i++;
                if (i == 8) i = mat.GetLength(1) - 8;
                if (tab[n] == 1) mat[8, i] = noir;
                else mat[8, i] = blanc;
                n++;
            }
            n = 0;
            for (int j = mat.GetLength(0) - 1; j >= 0; j--)
            {
                if (j == mat.GetLength(0) - 8) j = 8;
                if (j == 6) j--;
                if (tab[n] == 1) mat[j, 8] = noir;
                else mat[j, 8] = blanc;
                n++;
            }
            mat[mat.GetLength(0) - 8, 8] = noir;

        }

        /// <summary>
        /// Remplit une matrice de pixels avec un tableau binaire.
        /// </summary>
        /// <param name="mat">Matrice de pixels à remplir.</param>
        /// <param name="tab">Tableau binaire à utiliser pour le remplissage.</param>
        public void Remplissage(Pixel[,] mat, byte[] tab)
        {
            int i = mat.GetLength(0) - 1;
            int j = mat.GetLength(1) - 1;
            int index = 0;
            Pixel blanc = new Pixel(255, 255, 255);
            Pixel noir = new Pixel(0, 0, 0);
            bool monte = true;
            while (j > 0)
            {
                if (monte)
                {
                    while (i >= 0)
                    {
                        if (mat[i, j] == null)
                        {
                            mat[i, j] = tab[index] == 0 ? blanc : noir;
                            SurMasque(i, j, mat);
                            index++;
                        }
                        if (mat[i, j - 1] == null)
                        {
                            mat[i, j - 1] = tab[index] == 0 ? blanc : noir;
                            SurMasque(i, j - 1, mat);
                            index++;
                        }
                        i--;
                    }
                    i++;
                }
                else
                {
                    while (i < mat.GetLength(0))
                    {
                        if (mat[i, j] == null)
                        {
                            if (index >= tab.Length)
                            {
                                mat[i, j] = blanc;
                                SurMasque(i, j, mat);
                            }
                            else
                            {
                                mat[i, j] = tab[index] == 0 ? blanc : noir;
                                SurMasque(i, j, mat);
                                index++;
                            }


                        }
                        if (mat[i, j - 1] == null)
                        {
                            if (index >= tab.Length)
                            {
                                mat[i, j - 1] = blanc;
                                SurMasque(i, j - 1, mat);
                            }
                            else
                            {
                                mat[i, j - 1] = tab[index] == 0 ? blanc : noir;
                                index++;
                                SurMasque(i, j - 1, mat);
                            }

                        }
                        i++;
                    }
                    i--;
                }
                monte = !monte;
                j -= 2;
                if (j > 0) { if (mat[9, j] != null) j--; }
            }
        }

        /// <summary>
        /// Applique un masquage spécifique sur un pixel de la matrice.
        /// </summary>
        /// <param name="i">Position en ligne du pixel dans la matrice.</param>
        /// <param name="j">Position en colonne du pixel dans la matrice.</param>
        /// <param name="mat">Matrice de pixels où le masquage doit être appliqué.</param>
        public void SurMasque(int i, int j, Pixel[,] mat)
        {
            Pixel blanc = new Pixel(255, 255, 255);
            Pixel noir = new Pixel(0, 0, 0);
            if ((i + j) % 2 == 0)
            {
                mat[i, j] = mat[i, j].Equals(noir) ? blanc : noir;
            }
        }

        /// <summary>
        /// Affiche une matrice de pixels sous forme de texte représentant les pixels blancs et noirs.
        /// </summary>
        /// <param name="matrice">Matrice de pixels à afficher.</param>
        static void AfficherMatricePixel(Pixel[,] matrice)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    if (matrice[i, j] == null) Console.Write(0);
                    else
                    {
                        if (matrice[i, j].Rouge == 0)
                        {
                            Console.Write("██"); // caractère représentant un pixel noir
                        }
                        else if (matrice[i, j].Rouge == 255)
                        {
                            Console.Write("  "); // caractère représentant un pixel blanc
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
