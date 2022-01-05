// Liefert einen Fehler, würde in JS aber problemlos laufen.
// Kann auch transpiliert werden.
function demos() {
    let a = 5;              // wird als Number festgelegt
    let b;                  // Wird asl any festgelegt
    let c:string;           // Wird als string festgelegt
    let d = 'Hello World!'  // Wird asl string festgelegt

    let e = 12;
    e = 'Hello World!'      // Liefert hier einen Fehler

    console.log(a);
}
demos();
