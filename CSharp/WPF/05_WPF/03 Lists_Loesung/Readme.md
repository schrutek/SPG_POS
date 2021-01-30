# Listen und ObservableCollection
![View Model Demo App Ui](ViewModelDemoApp2Ui.png)

In diesem Beispiel sollen alle Personen in einer Liste dargestellt werden. Beim Klicken auf einen
Eintrag der Liste werden die Daten geladen. Diese Features werden durch eine *ListBox* bereitgestellt.
Da Person ein komplexer Typ ist, muss �ber ein *DataTemplate* die Anzeige in der ListBox gesteuert werden.
Folgendes Beispiel zeigt *Firstname* und *Lastname* untereinander an:

```xml
<ListBox DockPanel.Dock="Left" ItemsSource="{Binding Persons}" SelectedItem="{Binding CurrentPerson}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <DockPanel Margin="5 5 5 5">
                <StackPanel>
                    <TextBlock Text="{Binding Firstname}" />
                    <TextBlock FontWeight="Bold" Text="{Binding Lastname}" />
                </StackPanel>
            </DockPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

Dabei ist *Persons* die Collection von Personen in *MainViewModel*, *CurrentPerson* ist das Property in
*MainViewModel*, in welches die Liste die aktuell ausgew�hlte Person hineinschreibt. Es muss nat�rlich
daher ein public set Property sein.

F�r die Darstellung von Listen gibt es in WPF mehrere Controls:
![List Types](ListTypes.png)
*<sup>Quelle: http://www.sws.bfh.ch/~amrhein/Skripten/Info2/, Kapitel 8: WPF Listen und Tabellen</sup>*

## Erstellen der Collection mittels LINQ Abfrage aus dem Model
Hier wird in *get* des Properties eine LINQ Abfrage geschrieben, die die Daten aus dem Model holt. Gegebenenfalls
muss mit *ToList()* die Ausf�hrung erzwungen werden, damit z. B. die Daten aus der Datenbank gelesen werden.

In unserem Beispiel wird dies mittels folgendem Properties erledigt:
```c#
public IList<Person> Persons => personDb.Persons.ToList();
```

Werden nun die zugrundeliegenden Daten �ber die GUI ge�ndert (hinzuf�gen oder l�schen von Elementen), 
muss �ber *PropertyChanged()* die Liste neu eingelesen werden. Bei einer �nderung der Objekte selbst wird 
die �nderung sofort dargestellt, da es sich bei der Liste nur um Referenzen auf die Originalobjekte 
handelt. Dennoch muss folgendes beachtet werden:
- Der Aufruf von *PropertyChanged()* muss immer beim Hinzuf�gen oder L�schen erfolgen, um eine konsistente 
  Darstellung zu gew�hrleisten.
- *PropertyChanged()* liest die Liste zur G�nze neu ein. Bei einer langsamen Quelle (z. B. einem Webservice)
  kann hier eine Latenz f�r den Anwender entstehen, vor allem wenn sehr h�ufig Objekte manipuliert werden.

In unserem Beispiel wird das Einf�gen eines Datensatzes in *GeneratePersonCommand* durchgef�hrt:
```cs
GeneratePersonCommand = new RelayCommand(
    () =>
    {
        Random rnd = new Random();
        personDb.Persons.Add(new Person
        {
            Firstname = $"Vorname{rnd.Next(1000, 9999 + 1)}",
            Lastname = $"Zuname{rnd.Next(1000, 9999 + 1)}",
            Sex = rnd.Next(0, 2) == 0 ? Sex.Male : Sex.Female,
            DateOfBirth = DateTime.Now.AddDays(-rnd.Next(18 * 365, 25 * 365))
        });
        // Bewirkt das neue Auslesen der Liste.
        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Persons)));
    });
```
