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


/* -------Dark Mode & Light Mode-------------------*/

document.addEventListener('DOMContentLoaded', function () {
    let sw = document.querySelector('#switch-mode');

    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light";

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(res => {
                if (res.ok) {
                    window.location.reload();
                } else {
                    console.log('Something went wrong while processing the request.');
                }
            })
            .catch(error => {
                console.error('Error making the AJAX request:', error);
            });
    });

    let themeMode = document.cookie.replace(/(?:(?:^|.*;\s*)ThemeMode\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    let switchMode = document.getElementById('switch-mode');
    if (themeMode === "dark") {
        switchMode.checked = true;
    }
});