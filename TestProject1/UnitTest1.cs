namespace Image_Morganico_Dupin
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodAggrandissement()
        {
            MyImage im1 = new MyImage("mario.bmp");
            im1.Agrandir(4);
        }
        [TestMethod]
        public void TestMethodRetrecir()
        {
            MyImage im1 = new MyImage("mario.bmp");
            im1.Retrecir(4);
        }
        [TestMethod]
        public void TestMethodNuance()
        {
            MyImage im1 = new MyImage("mario.bmp");
            im1.NuanceGris();
        }
        [TestMethod]
        public void TestMethodSteganographie()
        {
            MyImage im1 = new MyImage("mario.bmp");
            im1.Steganographie("lena.bmp");
            im1.DeSteganographie();
        }
        [TestMethod]
        public void TestMethodMiroir()
        {
            MyImage im1 = new MyImage("mario.bmp");
            im1.Mirroir();
        }
        [TestMethod]
        public void TestMethodRotation()
        {
            MyImage im1 = new MyImage("mario.bmp");
            im1.Rotation(45);
        }
        [TestMethod]
        public void TestMethodHistogramme()
        {
            MyImage im1 = new MyImage("mario.bmp");
            im1.histogramme();
        }
        [TestMethod]
        public void TestRechercheTabChar()
        {
            QRCode im = new QRCode();
             char[] tableauDeCaracteres = {
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',' ',
                    '$', '%', '*', '+', '-', '.', '/', ':'};
            char t1 = 'F';
            char t2 = '§';
            int res = im.recherche_tabchar(tableauDeCaracteres, t1);
            int res2 = im.recherche_tabchar(tableauDeCaracteres, t2);
        }
        [TestMethod]
        public void TestDecimalToBinarie()
        {
            QRCode im = new QRCode();
            byte[] tab = im.DecimalToBinary(143, 8);
        }
        [TestMethod]
        public void TestAppliquerZoneMasquage()
        {
            QRCode im = new QRCode("HELLO WORLD");
            im.AppliquerZonesMasquage(im.QRCODE.Matrice, 1);
        }

        [TestMethod]
        public void TestRemplissage()
        {
            QRCode im = new QRCode("HELLO WORLD");
            byte[] tab = im.TableauQRCODE("HELLO WORLD", 1);
            im.Remplissage(im.QRCODE.Matrice, tab);
        }
    }
}