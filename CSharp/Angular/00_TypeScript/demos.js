// Funktioniert, da var in der function gilt
function log() {
    for (var i = 0; i < 5; i++) {
        console.log(i);
    }
    console.log('Final: ' + i);
}
log();
// Fehler, da let nur in der for-Schleife gilt
function log2() {
    for (var i = 0; i < 5; i++) {
        console.log(i);
    }
    console.log('Final: ' + i);
}
log2();
var Colours;
(function (Colours) {
    Colours[Colours["Red"] = 0] = "Red";
    Colours[Colours["Blue"] = 1] = "Blue";
    Colours[Colours["Green"] = 2] = "Green";
})(Colours || (Colours = {}));
var backgoundColor = Colours.Blue;
