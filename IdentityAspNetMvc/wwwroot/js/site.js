const nav = document.querySelector("#nav");
const abrir = document.querySelector("#abrir");
const cerrar = document.querySelector("#cerrar");

abrir.addEventListener("click", () => {
    nav.classList.add("visible");
    abrir.style.display = "none";
    cerrar.style.display = "block";
});

cerrar.addEventListener("click", () => {
    nav.classList.remove("visible");
    abrir.style.display = "block";
    cerrar.style.display = "none";
});
window.addEventListener("resize", () => {
    if (window.innerWidth >= 992) {
        abrir.style.display = "none";
        nav.classList.remove("visible");
        cerrar.style.display = "none";
    } else {
        abrir.style.display = "block";
    }
});
