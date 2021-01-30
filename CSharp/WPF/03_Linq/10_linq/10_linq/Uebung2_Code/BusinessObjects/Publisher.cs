using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
// Falls der Compiler  Fehler  bei  using System.Drawing und Bitmap   meldet
//  fehlt ein Verweis  (im Projektmappenexplorer rechte Maustaste auf Verweise -
//                      Verweis hinzufügen  (kurz Geduld) System.Drawing hinzufügen
// Verweise informieren den Compiler, welche Assemblys er durchsuchen soll
//          um die dort enthaltenen Klassen verwenden zu können. 

namespace LinqInAction.LinqBooks.Common
{
  public class Publisher
  {
    public String Name {get; set;}
    public Bitmap Logo {get; set;}
    public String WebSite {get; set;}

    public override string ToString()
    {
      return Name;
    }
  }
}
