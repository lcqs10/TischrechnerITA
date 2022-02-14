using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Tischrechner
{
    public partial class Tischrechner : Form
    {
        Rechnung Re = new Rechnung(); //Rechnungsklasse implementieren

        bool AnsUsed = false; //Ans genutzt?
        bool Negierung = false; //Negierung genutzt?
        string Pfad = "Data_" + DateTime.Now.ToShortDateString(); //Dateiname generieren (Data_[Datum].csv). Variable, weil im Programm änderbar

        string zwspeicher;
        bool juststarted = true; //um Speicherfehler zu vermeiden

        int r = 240;
        int g = 100; //für rgb (spielerei)
        int b = 240;
        int wo = 0;

        public Tischrechner()
        {
            InitializeComponent();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            if(sender == Settings || sender == SettLog)
                Main.SelectedIndex = 1;
            if (sender == Logs)
                Main.SelectedIndex = 2;
        }
        private void SetZumRech_Click(object sender, EventArgs e)
        {
            Main.SelectedIndex = 0;
        }
        private void bZahlenfeld_Click(object sender, EventArgs e)
        {

            //
            //ZAHLEN
            //

            if (sender == b1)
                {
                    Re.CurDigit = Re.CurDigit + "1";
                    Re.CurInvoice = Re.CurInvoice + "1";
                Re.CurInv.AddLast("1");
                if (!EasyMd.Checked)
                    Re.CurOperator = null; //Wenn eine Zahl eingegeben wurde, kann wieder ein neuer Operator verwendet werden. (Doppelte Operatoren vermieden)
                }
            if (sender == b2)
                {
                    Re.CurDigit = Re.CurDigit + "2";
                    Re.CurInvoice = Re.CurInvoice + "2";
                Re.CurInv.AddLast("2");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b3)
                {
                    Re.CurDigit = Re.CurDigit + "3";
                    Re.CurInvoice = Re.CurInvoice + "3";
                Re.CurInv.AddLast("3");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b4)
                {
                    Re.CurDigit = Re.CurDigit + "4";
                    Re.CurInvoice = Re.CurInvoice + "4";
                Re.CurInv.AddLast("4");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b5)
                {
                    Re.CurDigit = Re.CurDigit + "5";
                    Re.CurInvoice = Re.CurInvoice + "5";
                Re.CurInv.AddLast("5");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b6)
                {
                    Re.CurDigit = Re.CurDigit + "6";
                    Re.CurInvoice = Re.CurInvoice + "6";
                Re.CurInv.AddLast("6");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b7)
                {
                    Re.CurDigit = Re.CurDigit + "7";
                    Re.CurInvoice = Re.CurInvoice + "7";
                Re.CurInv.AddLast("7");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b8)
                {
                    Re.CurDigit = Re.CurDigit + "8";
                    Re.CurInvoice = Re.CurInvoice + "8";
                Re.CurInv.AddLast("8");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b9)
                {
                    Re.CurDigit = Re.CurDigit + "9";
                    Re.CurInvoice = Re.CurInvoice + "9";
                Re.CurInv.AddLast("9");
                if (!EasyMd.Checked)
                    Re.CurOperator = null;
            }
            if (sender == b0)
                {
                    if (Re.CurDigit != "0" && Re.CurDigit != null && Re.CurDigit != "") //Wenn die aktuelle Zahl nicht 0 ist, dann kann eine weitere Null hinzugefügt werden
                    {
                        Re.CurDigit = Re.CurDigit + "0";
                        Re.CurInvoice = Re.CurInvoice + "0";
                        Re.CurInv.AddLast("0");
                        if (!EasyMd.Checked)
                            Re.CurOperator = null;
                    }
                }
            if (sender == b00)
                {
                if (Re.CurDigit != "0" && Re.CurDigit != null && Re.CurDigit != "") 
                {
                    Re.CurDigit = Re.CurDigit + "00";
                    Re.CurInvoice = Re.CurInvoice + "00";
                    Re.CurInv.AddLast("00");
                    if (!EasyMd.Checked)
                        Re.CurOperator = null;
                }
            }
            if (sender == bPunkt)
                    {
                    if(Re.CurDigit == null)
                    {
                    Re.CurDigit = Re.CurDigit + "0,";
                    Re.CurInvoice = Re.CurInvoice + "0,";
                    Re.CurInv.AddLast("0,");
                    if (!EasyMd.Checked)
                        Re.CurOperator = null;
                    }
                    else if (!Re.CurDigit.Contains(","))
                    {
                        Re.CurDigit = Re.CurDigit + ",";
                        Re.CurInvoice = Re.CurInvoice + ",";
                    Re.CurInv.AddLast(",");
                    if (!EasyMd.Checked)
                        Re.CurOperator = null;
                    }
                }

            //
            //OPERATOREN
            //

            if (sender == bPlus)
            {
                if (Re.CurOperator == null && Re.CurDigit != null)
                {
                    if (EasyMd.Checked)
                        Ergebnis2.Text = ""; Re.CurOperator = "+";
                    Re.CurInvoice = Re.CurInvoice + " + "; //Mit Leerzeichen speichern, um für die Rechnungsmethoden lesbar zu machen
                    Re.CurInv.AddLast(" + ");
                    Re.CurDigit = null;
                    AnsUsed = false; //Ans kann wieder genutzt werden, weil ein Operator verwendet wurde. (um Eingaben wie "57575757" zu vermeiden)
                    Negierung = false;
                }
                else if (Re.CurDigit != null)
                {
                    if (EasyMd.Checked)
                    {
                        Ergebnis2.Text = "";
                        Ergebnis.Text = Re.Rechnen().ToString();
                        Re.CurOperator = "+";
                        Re.CurInvoice = Re.CurInvoice + " + ";
                    }
                }
            }
            if (sender == bMinus)
            {
                if (Re.CurOperator == null && Re.CurDigit != null)
                {
                    if (EasyMd.Checked)
                        Ergebnis2.Text = "";
                    Re.CurOperator = "-";
                    Re.CurInvoice = Re.CurInvoice + " - ";
                    Re.CurInv.AddLast(" - ");
                    Re.CurDigit = null;
                    AnsUsed = false;
                    Negierung = false;
                }
                else if (Re.CurDigit != null)
                {
                    if (EasyMd.Checked)
                    {
                        Ergebnis2.Text = "";
                        Ergebnis.Text = Re.Rechnen().ToString();
                        Re.CurOperator = "-";
                        Re.CurInvoice = Re.CurInvoice + " - ";
                    }
                }
            }
            if (sender == bMal)
            {
                if (Re.CurOperator == null && Re.CurDigit != null)
                {
                    if (EasyMd.Checked)
                        Ergebnis2.Text = "";
                    Re.CurOperator = "x";
                    Re.CurInvoice = Re.CurInvoice + " x ";
                    Re.CurInv.AddLast(" x ");
                    Re.CurDigit = null;
                    AnsUsed = false;
                    Negierung = false;
                }
                else if (Re.CurDigit != null)
                {
                    if (EasyMd.Checked)
                    {
                        Ergebnis2.Text = "";
                        Ergebnis.Text = Re.Rechnen().ToString();
                        Re.CurOperator = "x";
                        Re.CurInvoice = Re.CurInvoice + " x ";
                    }
                }
            }
            if (sender == bGeteilt)
            {
                if (Re.CurOperator == null && Re.CurDigit != null)
                {
                    if (EasyMd.Checked) 
                        Ergebnis2.Text = "";
                    Re.CurOperator = "÷";
                    Re.CurInvoice = Re.CurInvoice + " ÷ ";
                    Re.CurInv.AddLast(" ÷ ");
                    Re.CurDigit = null;
                    AnsUsed = false;
                    Negierung = false;
                }
                else if (Re.CurDigit != null)
                {
                    if (EasyMd.Checked)
                    {
                        Ergebnis2.Text = "";
                        Ergebnis.Text = Re.Rechnen().ToString();
                        Re.CurOperator = "÷";
                        Re.CurInvoice = Re.CurInvoice + " ÷ ";
                    }
                }
            }

            //
            //EXTRATASTEN
            //

            if(sender == bAns)
            {
                if (Re.AnsWert != null && AnsUsed == false) //wenn Ans nicht null ist und Die Ans noch nicht in dieser Zahl genutzt wurde
                {
                    Re.CurDigit = Re.CurDigit + Re.AnsWert; //Ans wird einfach dran gehangen.
                    Re.CurInvoice = Re.CurInvoice + Re.AnsWert;
                    Re.CurInv.AddLast(Re.AnsWert);
                    AnsUsed = true;
                    if (!EasyMd.Checked)
                        Re.CurOperator = null;
                }
            }
            if(sender == bProz)
            { //im Grunde das selbe wie jeder anderer Operator
                if (Re.CurOperator == null && Re.CurDigit != null)
                {
                    Re.CurInvoice = Re.CurInvoice + " % ";
                    Re.CurInv.AddLast(" % ");
                    Re.CurDigit = null;
                    AnsUsed = false;
                    Negierung = false;
                }
            }
            if(sender == bNeg)
            {
                try
                {
                    if (Re.CurInv.Last() != " + " && Re.CurInv.Last() !=  " - " && Re.CurInv.Last() != " x " && Re.CurInv.Last() != " ÷ " && Re.CurInv.Last() != " % ")
                    {
                        if (!Negierung) //wenn Negation nicht angewandt ist, dann mach ein - vor die Zahl
                        {
                            Re.CurDigit = "-" + Re.CurDigit;
                            zwspeicher = "-" + Re.CurInv.Last();
                            Re.CurInv.RemoveLast();
                            Re.CurInv.AddLast(zwspeicher);
                            Negierung = true;
                        }
                        else if (Negierung) //andernfalls mach das - weg
                        {
                            Re.CurDigit = Re.CurDigit.Remove(0, 1);
                            zwspeicher = Re.CurInv.Last().Remove(0, 1);
                            Re.CurInv.RemoveLast();
                            Re.CurInv.AddLast(zwspeicher);
                            Negierung = false;
                        }
                    }
                }
                catch { }
            }

            if (sender == bBack)
            {
                if(Re.CurInv.Count()!=0)
                    Re.CurInv.RemoveLast();
                //letztes Item aus Liste entfernen, wenn da überhaupt noch was drin ist

            }
            if (sender == bCA)
            {
                Clear();
            }
            if (sender == bC)
            {
                ClearLast();
            }
            DisplayRefresh();
            if (sender == bErg)
            {
                if (EasyMd.Checked)
                {
                    Re.Rechnen(); //wenn EasyMode an ist, dann EasyMethode
                    Ergebnis2.Text = Re.CurShow;
                }
                else
                if (!PvS.Checked)
                {
                    Ergebnis2.Text = Re.OhnePvS(Re.CurInvoice); 
                    Re.SaveData.AddLast(Re.CurInvoice + " = " + Ergebnis2.Text);
                    if (AutoSaveBox.Checked && Ergebnis2.Text != "" && Ergebnis2.Text != null)
                    {
                        Save();
                        Rechnungen.Items.Add(Re.CurInvoice + "= " + Ergebnis2.Text);
                    }
                    Re.CurInvoice = null;
                    Re.CurInv.Clear();
                    Re.CurDigit = null;

                }
                else if(PvS.Checked)
                {
                    Ergebnis2.Text = Re.PvS(Re.CurInvoice);
                    Re.SaveData.AddLast(Re.CurInvoice + " = " + Ergebnis2.Text);
                    if (AutoSaveBox.Checked && Ergebnis2.Text != "" && Ergebnis2.Text != null)
                    {
                        Save();
                        Rechnungen.Items.Add(Re.CurInvoice + " = " + Ergebnis2.Text);
                    }
                    Re.CurInvoice = null;
                    Re.CurInv.Clear();
                    Re.CurDigit = null;
                }

                

            }
        }

        //
        //SPIELEREIEN
        //

        private void Tischrechner_Load(object sender, EventArgs e)
        {
            Clear();
            LoadSave();
            PfadTextBox.Text = Pfad;
            sourcelbl.Text = "Quelle: " + Pfad;
            juststarted = false;
        }
        private void EasyMd_CheckedChanged(object sender, EventArgs e)
        {
            Clear();
            if (EasyMd.Checked)
            {
                PvS.Enabled = false;
                PvS.Checked = false;
                bBack.Visible = false;
                bAns.Visible = false;
                bProz.Visible = false;
                bNeg.Visible = false;
                

                CurMode.Text = "Einfacher Modus";
            }else if (!EasyMd.Checked)
            {
                PvS.Enabled = true;
                bBack.Visible = true;
                bAns.Visible = true;
                bProz.Visible = true;
                bNeg.Visible = true;
                EasyMdError.Visible = false;

                CurMode.Text = "Erweiterter Modus: Ohne Punkt vor Strich";
            }
        }
        private void PvS_CheckedChanged(object sender, EventArgs e)
        {
            Clear();

            if (PvS.Checked)
                CurMode.Text = "Erweiterter Modus: Mit Punkt vor Strich";
            if (!PvS.Checked)
                CurMode.Text = "Erweiterter Modus: Ohne Punkt vor Strich";
        }
        private void Rechnungen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Rechnungen.SelectedItem != null && !EasyMd.Checked)
                bLoadInv.Visible = true;
            else if (Rechnungen.SelectedItem == null && !EasyMd.Checked)
                bLoadInv.Visible = false;
            if (EasyMd.Checked)
            {
                EasyMdError.Visible = true;
                bLoadInv.Visible = false;
            }
            else
            {
                EasyMdError.Visible = false;
                bLoadInv.Visible = true;
            }
        }
        private void bLoadInv_Click(object sender, EventArgs e)
        {
            string[] Load = Convert.ToString(Rechnungen.SelectedItem).Split(' ');
            Re.CurInv.Clear();
            Re.CurInvoice = "";

            foreach (string s in Load)
            {
                if (s.Contains('=')) //Bei = ist die Rechnung zu ende. Also letzte " " zeichen entfernen und raus aus der foreach.
                {
                    if (s == "=")
                    {
                        Re.CurInvoice = Re.CurInvoice.Remove(Re.CurInvoice.Count() - 1, 1); //letztes " " zeichen aus der Rechnung löschen
                        string sd = Re.CurInv.Last(); //Last zwischenspeichern
                        Re.CurInv.RemoveLast(); //und entfernen
                        Re.CurInv.AddLast(sd.Remove(sd.Count() - 1, 1)); //letztes " " aus Inv Liste löschen
                    }
                        break; //foreach break, weil das Ergebnis nicht benötigt wird       
                }
                Re.CurInv.AddLast(s + " "); //Rechnung übernehmen aus Array
                Re.CurInvoice = Re.CurInvoice + s + " ";
                Re.CurDigit = s;
            }
            DisplayRefresh();
            Main.SelectedIndex = 0;            
        }
        private void DarkModeBox_CheckedChanged(object sender, EventArgs e)
        {
            //Darkmode
            Color DMBack = Color.FromArgb(42, 42, 46);
            Color DMFront = Color.FromArgb(32, 32, 36);
            Color DMText = Color.FromArgb(192, 192, 192);
            Color DMTitle = Color.FromArgb(224, 224, 224);
            Color DMMenuBt = Color.FromArgb(38, 38, 42);

            //Whitemode
            Color WMBack = Color.FromArgb(245, 245, 255);
            Color WMFront = Color.FromArgb(230, 230, 245);
            Color WMText = Color.FromArgb(63, 63, 63);
            Color WMTitle = Color.FromArgb(31, 31, 31);
            Color WMMenuBt = Color.FromArgb(235, 235, 245);


            if (DarkModeBox.Checked)
            {
                RechnerPage.BackColor = SettingsPage.BackColor = LogsPage.BackColor = Admin.BackColor = DMBack;
                Ergebnis.BackColor = Ergebnis2.BackColor = b1.BackColor = b2.BackColor = b3.BackColor = b4.BackColor = b5.BackColor = b6.BackColor = b7.BackColor = b8.BackColor = b9.BackColor = b0.BackColor = b00.BackColor = bPunkt.BackColor = FarbTestBt.BackColor = Ergebnis2.BackColor = bNeg.BackColor = bBack.BackColor = bC.BackColor = bCA.BackColor = bAns.BackColor = bProz.BackColor = bMal.BackColor = bPlus.BackColor = bMinus.BackColor = bGeteilt.BackColor = bErg.BackColor = DMFront;
                manSave.ForeColor = pathCreate.ForeColor = PfadTextBox.ForeColor = bLoadInv.ForeColor = Ergebnis.ForeColor = Ergebnis2.ForeColor = bNeg.ForeColor = bBack.ForeColor = bC.ForeColor = bAns.ForeColor = bProz.ForeColor = bPlus.ForeColor = bMinus.ForeColor = bMal.ForeColor = bGeteilt.ForeColor = bErg.ForeColor = Logs.ForeColor = Settings.ForeColor = Uhrzeitlbl.ForeColor = Uhrzeitlbl2.ForeColor = Uhrzeitlbl3.ForeColor = Uhrzeitlbl4.ForeColor = CurMode.ForeColor = AdminClick.ForeColor = SetZumRech.ForeColor = EasyMd.ForeColor = PvS.ForeColor = DarkModeBox.ForeColor = ClockOn.ForeColor = hiernichts.ForeColor = rgbBox.ForeColor = label1.ForeColor = AutoSaveBox.ForeColor = EasyMdError.ForeColor = LogBack.ForeColor = Rechnungen.ForeColor = SettLog.ForeColor = label4.ForeColor = DMText;
                SettingsUe.ForeColor = SettingsFunktion.ForeColor = SettingsDesign.ForeColor = label5.ForeColor = lblspeichern.ForeColor = label3.ForeColor = AdSite.ForeColor = DMTitle;
                PfadTextBox.BackColor = Rechnungen.BackColor = pathCreate.BackColor = manSave.BackColor = bLoadInv.BackColor = DMMenuBt;


            }
            else
            {
                RechnerPage.BackColor = SettingsPage.BackColor = LogsPage.BackColor = Admin.BackColor = WMBack;
                Ergebnis.BackColor = Ergebnis2.BackColor = b1.BackColor = b2.BackColor = b3.BackColor = b4.BackColor = b5.BackColor = b6.BackColor = b7.BackColor = b8.BackColor = b9.BackColor = b0.BackColor = b00.BackColor = bPunkt.BackColor = FarbTestBt.BackColor = Ergebnis2.BackColor = bNeg.BackColor = bBack.BackColor = bC.BackColor = bCA.BackColor = bAns.BackColor = bProz.BackColor = bMal.BackColor = bPlus.BackColor = bMinus.BackColor = bGeteilt.BackColor = bErg.BackColor = WMFront;
                manSave.ForeColor = pathCreate.ForeColor = PfadTextBox.ForeColor = bLoadInv.ForeColor = Ergebnis.ForeColor = Ergebnis2.ForeColor = bNeg.ForeColor = bBack.ForeColor = bC.ForeColor = bAns.ForeColor = bProz.ForeColor = bPlus.ForeColor = bMinus.ForeColor = bMal.ForeColor = bGeteilt.ForeColor = bErg.ForeColor = Logs.ForeColor = Settings.ForeColor = Uhrzeitlbl.ForeColor = Uhrzeitlbl2.ForeColor = Uhrzeitlbl3.ForeColor = Uhrzeitlbl4.ForeColor = CurMode.ForeColor = AdminClick.ForeColor = SetZumRech.ForeColor = EasyMd.ForeColor = PvS.ForeColor = DarkModeBox.ForeColor = ClockOn.ForeColor = hiernichts.ForeColor = rgbBox.ForeColor = label1.ForeColor = AutoSaveBox.ForeColor = EasyMdError.ForeColor = LogBack.ForeColor = Rechnungen.ForeColor = SettLog.ForeColor = label4.ForeColor = WMText;
                SettingsUe.ForeColor = SettingsFunktion.ForeColor = SettingsDesign.ForeColor = label5.ForeColor = lblspeichern.ForeColor = label3.ForeColor = AdSite.ForeColor = WMTitle;
                PfadTextBox.BackColor = Rechnungen.BackColor = pathCreate.BackColor = manSave.BackColor = bLoadInv.BackColor = WMMenuBt;


            }
        }
        private void AdminClick_Click(object sender, EventArgs e)
        {
            Main.SelectedIndex = 3;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string aktzeit = DateTime.Now.ToString("HH:mm:ss");
            Uhrzeitlbl.Text = aktzeit;
            Uhrzeitlbl2.Text = aktzeit;
            Uhrzeitlbl3.Text = aktzeit;
            Uhrzeitlbl4.Text = aktzeit;
        }
        private void AutoSaveBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoSaveBox.Checked)
            {
                manSave.Visible = false;
                manSavelbl.Text = "";
            }
            else
                manSave.Visible = true;
        }
        private void manSave_Click(object sender, EventArgs e)
        {
            Save();
            manSavelbl.Text = "Erfolgreich gespeichert!";
            errorclear.Enabled = true;
        }
        private void errorclear_Tick(object sender, EventArgs e)
        {
            manSavelbl.Text = "";
            errorclear.Enabled = false;
        }
        private void ClockOn_CheckedChanged(object sender, EventArgs e)
        {
            if (!ClockOn.Checked)
            {
                Uhrzeitlbl.Visible = false;
                Uhrzeitlbl2.Visible = false;
                Uhrzeitlbl3.Visible = false;
                Uhrzeitlbl4.Visible = false;

                timer1.Enabled = false;
            }
            else
            {
                Uhrzeitlbl.Visible = true;
                Uhrzeitlbl2.Visible = true;
                Uhrzeitlbl3.Visible = true;
                Uhrzeitlbl4.Visible = true;
                timer1_Tick(ClockOn, e);

                timer1.Enabled = true;
            }
        }
        private void FarbenClick(object sender, EventArgs e)
        {
            Color cl = Color.FromArgb(255, 128, 128);
            Color cl2 = Color.FromArgb(128, 64, 64);
            if (sender == fLachs)
            {
                cl = Color.FromArgb(255, 128, 128);
                cl2 = Color.FromArgb(128, 64, 64);
            }
            else if (sender == fOrange)
            {
                cl = Color.FromArgb(255, 192, 128);
                cl2 = Color.FromArgb(128, 96, 64);
            }
            else if (sender == fGelb)
            {
                cl = Color.FromArgb(255, 255, 128);
                cl2 = Color.FromArgb(128, 128, 64);
            }
            else if (sender == fGruen)
            {
                cl = Color.FromArgb(128, 255, 128);
                cl2 = Color.FromArgb(64, 128, 64);
            }
            else if (sender == fTurk)
            {
                cl = Color.FromArgb(128, 255, 255);
                cl2 = Color.FromArgb(64, 128, 128);
            }
            else if (sender == fBlau)
            {
                cl = Color.FromArgb(128, 128, 255);
                cl2 = Color.FromArgb(64, 64, 128);
            }
            else if (sender == fPink)
            {
                cl = Color.FromArgb(255, 128, 255);
                cl2 = Color.FromArgb(128, 64, 128);
            }
            else if (sender == fGrau)
            {
                cl = Color.FromArgb(224, 224, 224);
                cl2 = Color.FromArgb(112, 112, 112);

                if (!DarkModeBox.Checked)
                {
                    cl = Color.FromArgb(31, 31, 31);
                    cl2 = Color.FromArgb(15, 15, 15);
                }
            }
            b1.ForeColor = b2.ForeColor = b3.ForeColor = b4.ForeColor = b5.ForeColor = b6.ForeColor = b7.ForeColor = b8.ForeColor = b9.ForeColor = b0.ForeColor = b00.ForeColor = bPunkt.ForeColor = FarbTestBt.ForeColor = Ergebnis2.ForeColor = cl;
            bCA.ForeColor = cl2;


        }
        private void rgbBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rgbBox.Checked)
            {
                fLachs.Visible = false;
                fOrange.Visible = false;
                fGelb.Visible = false;
                fGruen.Visible = false;
                fTurk.Visible = false;
                fBlau.Visible = false;
                fPink.Visible = false;
                fGrau.Visible = false;
                aktfarbe.Visible = true;
                rgbTimer.Enabled = true;
                rgbBar.Visible = true;
            }
            else
            {
                fLachs.Visible = true;
                fOrange.Visible = true;
                fGelb.Visible = true;
                fGruen.Visible = true;
                fTurk.Visible = true;
                fBlau.Visible = true;
                fPink.Visible = true;
                fGrau.Visible = true;
                aktfarbe.Visible = false;
                rgbTimer.Enabled = false;
                rgbBar.Visible = false;
            }
        }
        private void rgbTimer_Tick(object sender, EventArgs e)
        {
            switch (wo)
            {

                case 0:
                    r += 10;
                    if (r >= 250)
                    {
                        wo = 1;
                        break;
                    }
                    break;

                case 1:
                    g -= 10;
                    if (g <= 100)
                    {
                        wo = 2;
                        break;
                    }
                    break;
                case 2:
                    b += 10;
                    if (b >= 250)
                    {
                        wo = 3;
                        break;
                    }
                    break;
                case 3:
                    r -= 10;
                    if (r <= 100)
                    {
                        wo = 4;
                        break;
                    }
                    break;
                case 4:
                    g += 10;
                    if (g >= 250)
                    {
                        wo = 5;
                        break;
                    }
                    break;
                case 5:
                    b -= 10;
                    if (b <= 100)
                    {
                        wo = 0;
                        break;
                    }
                    break;

            }

            aktfarbe.Text = "r" + Convert.ToString(r) + " g" + Convert.ToString(g) + " b" + Convert.ToString(b);
            Color rgbColor = Color.FromArgb(r, g, b);
            if(!DarkModeBox.Checked)
                rgbColor = Color.FromArgb(r - 40, g -40, b -40);

            b1.ForeColor = b2.ForeColor = b3.ForeColor = b4.ForeColor = b5.ForeColor = b6.ForeColor = b7.ForeColor = b8.ForeColor = b9.ForeColor = b0.ForeColor = b00.ForeColor = bPunkt.ForeColor = FarbTestBt.ForeColor = Ergebnis2.ForeColor = rgbColor;
            if(DarkModeBox.Checked)
                bCA.ForeColor = Color.FromArgb(r / 2, g / 2, b / 2);
            if(!DarkModeBox.Checked)
                bCA.ForeColor = Color.FromArgb(r, g, b);
        }
        private void rgbBar_Scroll(object sender, EventArgs e)
        {
            //rgb schnelligkeit // rgbTimer ändern

            rgbTimer.Interval = (rgbBar.Value + 1) * 10;
        }

        //
        //METHODEN
        //

        private void DisplayRefresh()
        {
            if (!EasyMd.Checked) //Bei Erweitertem Modus
            {

                //Alle Werte auf Null
                Re.CurShow = "";
                Re.CurShow2 = "";
                Re.CurInvoice = "";

                foreach (string s in Re.CurInv) //Rechnung wird aus dem RechnungsArray errechnet.
                {
                    Re.CurInvoice = Re.CurInvoice + s;

                    //Zeilensystem (nicht optimal)
                    if (Re.CurShow.Length <= 22)
                        Re.CurShow = Re.CurShow + s;
                    if (Re.CurShow.Length >= 23)
                        Re.CurShow2 = Re.CurShow2 + s;
                }
                Ergebnis.Text = Re.CurShow;
                Ergebnis2.Text = Re.CurShow2;
            }
            else
                Ergebnis.Text = Re.CurInvoice; //Beim EasyMode ist die Anzeige einfach die aktuelle Rechnung.
        }
        private void Clear()
        {
            //alles clearen halt
            Re.CurDigit = null;
            Re.CurInvoice = null;
            Re.CurShow = null;
            Re.CurInv.Clear();
            Re.CurOperator = null;
            Re.CurShow2 = null;
            Re.WhInvoice = null;
            Ergebnis2.Text = "";
            AnsUsed = false;


            DisplayRefresh();
        }
        private void ClearLast()
        {
            if (EasyMd.Checked) //geht nur beim EasyMode
            {

                try
                {
                    string[] Array = Re.CurInvoice.Split(' ');
                    Clear();
                    if (Array[2] == "")
                        Array[1] = "";
                    if (Array[2] != "")
                        Array[2] = "";


                    Re.CurInvoice = "";
                    foreach (string s in Array)
                    {
                        if (s != "")
                            Re.CurInvoice = Re.CurInvoice + s + " ";
                    }
                }
                catch { }
                DisplayRefresh();
            }
        }
        public void Save()
        {
            if (File.Exists(@"" + Pfad + ".csv")) //Wenn die File schon existiert, dann
                File.WriteAllLines(@"" + Pfad + ".csv", Re.SaveData); //SaveData Liste einschreiben
        }
        public void LoadSave()
        {
            if (File.Exists(@"" + Pfad + ".csv"))
            {
                Re.SaveData.Clear();
                foreach (string s in File.ReadAllLines(@"" + Pfad + ".csv")) //Alles lesen
                {
                    //In SaveData Liste und in Verlauf Anzeige alle Items adden
                    Re.SaveData.AddLast(s);
                    Rechnungen.Items.Add(s);

                    if (s == "+" || s == "-" || s == "x" || s == "÷" || s == "%")
                        //Aktuellen Operator aktualisieren
                        Re.CurOperator = s;

                }
            }
            else
                File.Create(@"" + Pfad + ".csv"); //Wenn Datei nicht existiert, dann erstell die
            //Pfad: Data_[Aktuelles Datum].csv
        }
        private void PfadTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(@"" + PfadTextBox.Text + ".csv") && !juststarted)
            {
                if (Pfad == PfadTextBox.Text)
                    Pfad = PfadTextBox.Text;
                FehlerLbl.Text = "";
                pathCreate.Visible = false;
                sourcelbl.Text = "Quelle: " + Pfad;
                Rechnungen.Items.Clear();
                Re.SaveData.Clear();
                foreach (string s in File.ReadAllLines(@"" + PfadTextBox.Text + ".csv")) //Alles lesen
                {
                    //In SaveData Liste und in Verlauf Anzeige alle Items adden
                    Re.SaveData.AddLast(s);
                    Rechnungen.Items.Add(s);

                    if (s == "+" || s == "-" || s == "x" || s == "÷" || s == "%")
                        //Aktuellen Operator aktualisieren
                        Re.CurOperator = s;

                }
            }
            else if(!juststarted)
            {
                FehlerLbl.Text = "Diese Datei existiert nicht.";
                pathCreate.Visible = true;                
            }

        }
        private void pathCreate_Click(object sender, EventArgs e)
        {
            if(!File.Exists(@"" + PfadTextBox.Text + ".csv") && PfadTextBox.Text != "")
            {
                File.Create(@"" + PfadTextBox.Text + ".csv");
                Pfad = PfadTextBox.Text;
                FehlerLbl.Text = "";
                sourcelbl.Text = "Quelle: " + Pfad;
                pathCreate.Visible = false;
                Re.SaveData.Clear();
                Rechnungen.Items.Clear();
            }
        }
    }
}