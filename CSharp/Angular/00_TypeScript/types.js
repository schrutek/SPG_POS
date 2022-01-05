// Liefert einen Fehler, würde in JS aber problemlos laufen.
// Kann auch transpiliert werden.
function demos() {
    var a = 5; // wird als Number festgelegt
    var b; // Wird asl any festgelegt
    var c; // Wird als string festgelegt
    var d = 'Hello World!'; // Wird asl string festgelegt
    var e = 12;
    e = 'Hello World!'; // Liefert hier einen Fehler
    console.log(a);
}
demos();
