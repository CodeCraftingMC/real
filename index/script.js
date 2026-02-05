
const form = document.getElementById('contact');
const result = document.getElementById('result');
const clear = document.getElementById('clear');

form.addEventListener('submit', e => {
    e.preventDefault();
    const name = form.name.value.trim();
    const email = form.email.value.trim();
    if (!name || !email || !/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email)) {
        result.textContent = 'Please include your Name and E-Mail.';
        return;
    }
    result.textContent = 'Thank you. Your report has been sent to our flexible mailbox.';
    form.reset();
});

clear.addEventListener('click', () => { form.reset(); result.textContent = 'Cleared.' });



const themeToggle = document.getElementById('theme-toggle');
const html = document.documentElement;

// Check saved preference
if (localStorage.getItem('theme') === 'dark') {
    html.setAttribute('data-theme', 'dark');
    themeToggle.textContent = 'Light Mode';
}

themeToggle.addEventListener('click', () => {
    if (html.getAttribute('data-theme') === 'dark') {
        html.removeAttribute('data-theme');
        themeToggle.textContent = 'Dark Mode';
        localStorage.setItem('theme', 'light');
    } else {
        html.setAttribute('data-theme', 'dark');
        themeToggle.textContent = 'Light Mode';
        localStorage.setItem('theme', 'dark');
    }
});

const navoffset = document.querySelector('.navdiv');
document.documentElement.style.setProperty(
    '--nav-height',
    navoffset.offsetHeight + 'px'
);