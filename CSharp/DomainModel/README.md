# Domain Model

Einige Tipps die man beachten sollte, wenn das *Domain Model* erstellt wird.

Das *Domain Model* ist der zentrale Teil der Applikation und ist ähnlich, wie die Datenbank, langlebig. Es ist die objektorientierte Abbildung der Datenbank und sollte daher vor beginn der programmierarbeiten gründlich durchdacht werden.

## Regeln bei der Erstellung

Bei der Erstellung eines Domain Models sind folgende Regeln zu beachten:

### Sichere Listen

Wichtig bei der klassischen *1..n* Relation.

* Ein Backing Field anlegen. `private List<ShoppingCartItem> _shoppingCartItems = new();`. Hier wird die tatsächliche Liste gespeichert.
* Das Interface ``IReadOnlyList<..>`` verwenden. Das gibt den Inhalt des Backing-Fields zurück. **Grund**: Listen sollen nur aus der Datenbank befüllt werden können, nicht durch die Applikation.
* ``private set`` für alle Properties, die nicht verändert werden sollen. ``set`` (``private set``) ist aber notwendig, da es der OR-Mapper benötigt. **Grund**: Properties die aus der DB kommen, sollen in der Applikation nicht geändert/überschrieben werden können (z.B. Primary Key's).
* Konstruktoren anlegen, vor allem für jene Properties, die einen *private setter* haben. **Grund**: Der Konstruktor ist dafür da, dass kein unvollständiges Objekt erstellt werden kann.

