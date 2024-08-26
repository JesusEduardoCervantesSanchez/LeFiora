import Vibrant from 'vibrant'

document.addEventListener("DOMContentLoaded", function () {
    const sections = document.querySelectorAll('.section');

    sections.forEach(section => {
        const image = new Image();
        image.src = section.style.backgroundImage.replace(/url\(['"]?(.*?)['"]?\)/i, "$1");

        image.onload = function () {
            const vibrant = new Vibrant(image);
            const swatches = vibrant.swatches();
            const backgroundColor = swatches['Vibrant'] ? swatches['Vibrant'].getHex() : null;

            if (backgroundColor) {
                const textColor = getContrastYIQ(backgroundColor);
                section.style.backgroundColor = backgroundColor;

                // Ajustar el color de texto del navbar que está fuera del section
                const navbar = document.querySelector('.navbar');
                if (navbar) {
                    navbar.style.color = textColor;
                }
            }
        };
    });

    // Ejecutar la función al cargar y al hacer scroll
    changeNavbarTextColor();
    window.addEventListener('scroll', changeNavbarTextColor);

    function getContrastYIQ(hexcolor) {
        const r = parseInt(hexcolor.substr(1, 2), 16);
        const g = parseInt(hexcolor.substr(3, 2), 16);
        const b = parseInt(hexcolor.substr(5, 2), 16);
        const yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
        return (yiq >= 128) ? '#000000' : '#ffffff'; // Devuelve negro o blanco dependiendo del contraste
    }
});

