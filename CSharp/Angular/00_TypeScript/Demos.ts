// Funktioniert, da var in der function gilt
function log() {
    for (var i = 0 ; i< 5 ; i++) {
        console.log(i)
    }
    console.log('Final: ' + i)
}
log();


// Fehler, da let nur in der for-Schleife gilt
function log2() {
    for (let i = 0 ; i< 5 ; i++) {
        console.log(i)
    }
    console.log('Final: ' + i)
}
log2();


enum Colours {Red, Blue, Green}
let backgoundColor = Colours.Blue