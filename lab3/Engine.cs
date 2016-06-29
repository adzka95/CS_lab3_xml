using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab3
{
    [XmlType("Engine")]
    public class Engine
    {

        public double displacement;
        public double horsePower;
        [XmlAttribute("model")]
        public string model;

        public Engine() { }
        public Engine(double przemieszczenie, double konie, string nazwa) {
            displacement = przemieszczenie;
            horsePower = konie;
            model = nazwa;     

        }

        public void setDisplacement(double liczba) {
            displacement = liczba;
        }
        public double getDisplacement() {
            return displacement;
        }
        public void setHorse(double liczba) {
            horsePower = liczba;
        }
        public double getHorse() {
            return horsePower;
        }
        public void setModel(string nazwa) {
            model = nazwa;
        }
        public string getModel() {
            return model;
        }
    }
}
