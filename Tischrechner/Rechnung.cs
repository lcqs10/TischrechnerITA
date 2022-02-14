using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tischrechner
{
    class Rechnung
    {

        LinkedList<string> Zahl = new LinkedList<string>(); //Zahl LinkedList (Für PvS und OhnePvS)
        Queue<string> Operator = new Queue<string>(); //Operatoren Queue (Für PvS und OhnePvS)

        public string CurDigit; //Aktuelle Zahl (Für Einfachen Modus)
        public string WhInvoice; //Gesamte Rechnung (Für Einfachen Modus)
        public string CurOperator; //Aktueller Operator (Um aufeinanderfolgende Operatoren zu vermeiden)
        public string CurInvoice; //Aktuelle Rechnung (Für Einfachen und Erweiterten Modus)
        public string CurShow; //Aktuelle Anzeige (Für Die erste Zeile der Anzeige)
        public string CurShow2; //Aktuelle Anzeige 2 (Für Die zweite Zeile der Anzeige)
        public string AnsWert; //Wertspeicher (Für die "Ans" Taste)

        public LinkedList<string> CurInv = new LinkedList<string>(); //Aktuelle Rechnung als Liste Für die Backspace Taste
        public LinkedList<string> SaveData = new LinkedList<string>(); //Zu Speichernde Daten als Liste, weil die File.WriteAllLines Methode eine Liste oder Array braucht

        public string OhnePvS(string Rechnung)
        {
            //Rechnung ohne PvS
            
            string Erg = Rechnung;
            try
            {
                //Rechnung (Operatoren und Zahlen mit einem Leerzeichen getrennt) wird bei diesen gesplittet und in Array gepackt.
                string[] Arr = Rechnung.Split(' ');

                foreach (string s in Arr)
                {
                    //wenn der Array teil ein Operator ist, dann wird dieser auf die Operator Queue getan.
                    if (s == "+" || s == "-" || s == "x" || s == "÷" || s == "%")
                        Operator.Enqueue(s);
                    else
                        //wenn der Array teil kein Operator ist, kann es nur noch eine Zahl sein und wird dementsprechend auf die Zahl LinkedList getan.
                        Zahl.AddLast(s);

                }
            }
            catch { return Rechnung; }


            double Dig1 = 0;
            double Dig2 = 0;

            //wenn es noch mehr als einen Operator gibt, wird gerechnet.
            while (Operator.Count >= 1)
            {
                //ist der Nächste Operator ein + ?
                if (Operator.Peek() == "+")
                {
                    //Operator aus der Queue löschen
                    Operator.Dequeue();

                    //Erste Zahl aus der Liste nehmen
                    Dig1 = Convert.ToDouble(Zahl.First());
                    //und löschen.
                    Zahl.RemoveFirst();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    //Erste + Zweite Zahl
                    Erg = Convert.ToString(Dig1 + Dig2);
                    //Ergebnis zur Liste hinzufügen
                    Zahl.AddFirst(Erg + " ");

                    //AnsWert für die "Ans" Taste
                    AnsWert = Erg;
                }
                else
                if (Operator.Peek() == "-")
                {
                    Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Erg = Convert.ToString(Dig1 - Dig2);
                    Zahl.AddFirst(Erg + " ");
                    AnsWert = Erg;
                }
                else
                if (Operator.Peek() == "x")
                {
                    Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Erg = Convert.ToString(Dig1 * Dig2);
                    Zahl.AddFirst(Erg + " ");
                    AnsWert = Erg;
                }
                else
                if (Operator.Peek() == "÷")
                {
                    Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Erg = Convert.ToString(Dig1 / Dig2);
                    Zahl.AddFirst(Erg + " ");
                    AnsWert = Erg;
                }
                else
                if(Operator.Peek() == "%")
                {
                    Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Erg = Convert.ToString(Dig1 * Dig2 / 100);
                    Zahl.AddFirst(Erg + " ");
                    AnsWert = Erg;
                }
            }

            Zahl.Clear();
            Operator.Clear();
            Dig1 = 0;
            Dig2 = 0;

            return Erg;
        }
        public string PvS(string Rechnung)
        {
            string Invoice = "";
            string Erg = Rechnung;
            try
            {
                //Schon bei OhnePvS erklärt
                string[] Arr = Rechnung.Split(' ');

                foreach (string s in Arr)
                {
                    if (s == "+" || s == "-" || s == "x" || s == "÷" || s == "%")
                        Operator.Enqueue(s);
                    else
                        Zahl.AddLast(s);
                }
            }
            catch { return Rechnung; }


            double Dig1 = 0;
            double Dig2 = 0;



            while (Operator.Count >= 1)
            {
                if (Operator.Peek() == "+")
                {
                    // + wird entfernt
                    Operator.Dequeue();

                    //Zahlen werden Gespeichert und entfernt
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();

                    if (Operator.Count >= 1)
                    {
                        //Wenn der nächste Operator ein Punkt-Operator ist, muss dieser zuerst gerechnet werden.
                        if (Operator.Peek() == "x" || Operator.Peek() == "÷" || Operator.Peek() == "%")
                        {
                            if (Operator.Peek() == "x")
                            {
                                //Zahl2 wird neu gerechnet (Hier die Multiplikation) und Die 2. Multiplikationszahl auch gelöscht
                                Dig2 = Dig2 * Convert.ToDouble(Zahl.First());
                                Zahl.RemoveFirst();
                                //Mal wird gelöscht
                                Operator.Dequeue();
                            }
                            else
                            if (Operator.Peek() == "÷")
                            {
                                Dig2 = Dig2 / Convert.ToDouble(Zahl.First());
                                Zahl.RemoveFirst();
                                Operator.Dequeue();
                            }
                            else
                            if (Operator.Peek() == "%")
                            {
                                Dig2 = Dig2 * Convert.ToDouble(Zahl.First()) / 100;
                                Zahl.RemoveFirst();
                                Operator.Dequeue();
                            }
                        }
                    }
                    Invoice = Invoice + " " + Dig1 + "+" + Dig2;
                    //1. + 2. Zahl. 
                    //Wenn der nächste Operator ein Punkt war, dann ist die 2. Zahl hier das Ergebnis der Punkt-Rechnung.
                    Erg = Convert.ToString(Dig1 + Dig2);
                    //Ergebnis wird an die Liste vorn angehangen.
                    Zahl.AddFirst(Erg);

                }
                else
                if (Operator.Peek() == "-")
                {
                    Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();

                    if (Operator.Count >= 1)
                    {
                        if (Operator.Peek() == "x" || Operator.Peek() == "÷" || Operator.Peek() == "%")
                        {
                            if (Operator.Peek() == "x")
                            {
                                Dig2 = Dig2 * Convert.ToDouble(Zahl.First());
                                Zahl.RemoveFirst();
                                Operator.Dequeue();
                            }
                            else
                            if (Operator.Peek() == "÷")
                            {
                                Dig2 = Dig2 / Convert.ToDouble(Zahl.First());
                                Zahl.RemoveFirst();
                                Operator.Dequeue();
                            }
                            else
                            if (Operator.Peek() == "%")
                            {
                                Dig2 = Dig2 * Convert.ToDouble(Zahl.First()) / 100;
                                Zahl.RemoveFirst();
                                Operator.Dequeue();
                            }
                        }
                    }
                    Invoice = Invoice + " " + Dig1 + "+" + Dig2;
                    Erg = Convert.ToString(Dig1 - Dig2);
                    Zahl.AddFirst(Erg);
                }
                else
                if (Operator.Peek() == "x")
                {
                    Invoice = Invoice + Zahl.First() + Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Invoice = Invoice + Zahl.First();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Erg = Convert.ToString(Dig1 * Dig2);
                    Zahl.AddFirst(Erg + " ");
                    Invoice = Invoice + "= ";
                }
                else
                if (Operator.Peek() == "÷")
                {
                    Invoice = Invoice + Zahl.First() + Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Invoice = Invoice + Zahl.First();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Erg = Convert.ToString(Dig1 / Dig2);
                    Zahl.AddFirst(Erg + " ");
                    Invoice = Invoice + "= ";
                }
                else
                if (Operator.Peek() == "%")
                {
                    Invoice = Invoice + Zahl.First() + Operator.Dequeue();
                    Dig1 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Invoice = Invoice + Zahl.First();
                    Dig2 = Convert.ToDouble(Zahl.First());
                    Zahl.RemoveFirst();
                    Erg = Convert.ToString(Dig1 * Dig2 / 100);
                    Zahl.AddFirst(Erg + " ");
                    Invoice = Invoice + "= ";
                }
            }

            //SPEICHERN

            //CurInvoice = null;
            Console.WriteLine("Rechnung: " + Invoice);
            Zahl.Clear();
            Operator.Clear();
            Dig1 = 0;
            Dig2 = 0;
            AnsWert = Erg;
            return Erg;
        }
        public double Rechnen()
        {
            double ReErg = 0;

            try
            {
                //Für Einfachen Modus. Hier sind maximal 2 Zahlen und ein Operator möglich.
                //Split in Array (3 Einträge: 
                //[0] Erste Zahl,   [1] Operator,    [2] Zweite Zahl
                string[] Arr = CurInvoice.Split(' ');


                if (CurOperator != null)
                {
                    if (Arr[1] == "+")
                    {
                        //Array[0] + Array[2]
                        ReErg = Convert.ToDouble(Arr[0]) + Convert.ToDouble(Arr[2]);
                    }
                    if (Arr[1] == "-")
                    {
                        ReErg = Convert.ToDouble(Arr[0]) - Convert.ToDouble(Arr[2]);
                    }
                    if (Arr[1] == "x")
                    {
                        ReErg = Convert.ToDouble(Arr[0]) * Convert.ToDouble(Arr[2]);
                    }
                    if (Arr[1] == "÷")
                    {
                        ReErg = Convert.ToDouble(Arr[0]) / Convert.ToDouble(Arr[2]);
                    }
                }

                //Gesamte Rechnung wird mitprotokolliert
                //WhInvoice kann theoretisch mit gespeichert werden.
                WhInvoice = WhInvoice + " " + CurOperator + " " + CurInvoice;
                CurInvoice = ReErg.ToString();
                CurDigit = "0";
                CurOperator = null;

                CurShow = ReErg.ToString();
            } catch { }
            return ReErg;   
        }
    }
}