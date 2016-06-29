using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;

namespace lab3
{
    class Program
    {
        static public string sciezka = "C:\\Users\\Ada\\Documents\\Visual Studio 2012\\Projects\\Numerki\\lab3\\CarsCollection.xml";
       
        static void zad1(List<Car> myCars) {
            var wyniki = myCars
                   .Where(samochod => samochod.getModel().Equals("A6"))
                   .Select(samochod => new
                   {
                       hppl = samochod.getMotor().getHorse() / samochod.getMotor().getDisplacement(),
                       engineType = samochod.getMotor().getModel().Equals("TDI") ? "diesel" : "petrol"
                   });
            foreach (var sam in wyniki)
            {
                Console.WriteLine("{0} {1}", sam.engineType, sam.hppl);
            }
            Console.WriteLine("");
            var statystyki = wyniki.GroupBy(okaz => okaz.engineType)
                .Select(cos => new { typ = cos.Key, srednia = cos.Average(a => a.hppl) });

            foreach (var okaz in statystyki)
            {
                Console.WriteLine("{0} {1}", okaz.typ, okaz.srednia);
            }        }        static public void createXmlFromLinq(List<Car> myCars) {
            IEnumerable<XElement> nodes = from sam in myCars
                                          select new XElement("car",
                                              new XElement("model", sam.getModel()),
                                              new XElement("year", sam.getYear()),
                                              new XElement("engine",
                                                  new XAttribute("model", sam.getMotor().getModel()),
                                                  new XElement("displacement", sam.getMotor().getDisplacement()),
                                                  new XElement("horsePower", sam.getMotor().getHorse())));

            XElement rootNode = new XElement("cars", nodes); 
            rootNode.Save("CarsFromLinq.xml");
        }

        static public void zmien() {
            XDocument html = XDocument.Load(sciezka);
            var elementy = from el in html.Elements("CARS").Elements("car") select el;

            foreach (XElement sam in elementy) {
                sam.XPathSelectElement("./model").SetAttributeValue("year", sam.XPathSelectElement("./year").Value);
                sam.XPathSelectElement("./year").Remove();
                sam.XPathSelectElement("./engine/horsePower").Name = "hp";
            
            }

            html.Save("nowy.xml");
        
        }


        static public void html(List<Car> myCars)
        {
            string tab = "<table border=\"3\" style=\"background-color: #00FFFF\">";
                foreach(var sam in myCars){
                tab+="<tr><td>"+sam.getModel()+"</td>";
                    tab+="<td>"+sam.getMotor().getModel()+"</td>";
                    tab+="<td>"+sam.getMotor().getDisplacement()+"</td>";
                    tab+="<td>"+sam.getMotor().getHorse()+"</td>";
                    tab+="<td>"+sam.getYear()+"</td></tr>";
                }
             tab+="</table>";

            using (FileStream strumien = new FileStream("strona.html", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(strumien, Encoding.UTF8))
                {
                    w.WriteLine(tab);
                }
            }                          }
        static void Main(string[] args)
        {
            
                 List<Car> myCars = new List<Car>(){
            new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
            new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
            new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
            new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
            new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
            new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
            new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
            new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
            new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
        };
                 zad1(myCars);


                 XmlSerializer s = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("CARS"));
                 StreamWriter writer = new StreamWriter(sciezka);
                 s.Serialize(writer, myCars);
                 writer.Close();

                 XmlSerializer d = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("CARS"));
                 StreamReader reader = new StreamReader(sciezka);
                 myCars = (List<Car>)d.Deserialize(reader);     
                


                 XElement rootNode = XElement.Load(sciezka);
                 string jeden = " sum(/car/engine[@model!='TDI']/horsePower/text()) div count(/car/engine[@model!='TDI']/horsePower)";                 
                 double liczba = (double)rootNode.XPathEvaluate(jeden);
                 Console.WriteLine("\nSrednia moc: {0} \n" , liczba);

                 string drugi = "/car/model[not(. = preceding::text())]";
                 var models = rootNode.XPathSelectElements(drugi);
                 foreach (var i in models)
                     Console.WriteLine(i.Value);
                 Console.WriteLine("");
                 createXmlFromLinq(myCars);
                 html(myCars);
                 zmien();

            Console.WriteLine("");
            }
    }
}
