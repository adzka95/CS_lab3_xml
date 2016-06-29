using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab3
{
    [XmlType("car")]
    public class Car
    {
        [XmlElement("model")]
        public string model;
        [XmlElement("year")]
        public int year;
        [XmlElement("engine")]
        public Engine motor;


        public Car() { }
        public Car(string nazwa, Engine silnik, int rok) {
            model = nazwa;
            motor = silnik;
            year = rok;
        
        }

        public void setModel(string nazwa) {
            model = nazwa;
        }
        public string getModel() {
            return model;
        }
        public void setYear(int rok) {
            year = rok;
        }
        public int getYear() {
            return year;
        }
        public void setMotor(Engine silnik) {
            motor = silnik;
        }
        public Engine getMotor() {
            return motor;
        }

    }
}
