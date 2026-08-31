# GymRats — Updated Design System (Dark Navy & Crimson Edition)

Paste this whole file into whatever AI tool you're using to generate a view, so every page matches your current dark navy and crimson aesthetic, custom font choices, and layout standards.

## Colors
| Name | Hex | Use for |
|---|---|---|
| Primary / Accent (Crimson Red) | `#b91c1c` | Active underlines, badges, hover accents, footer accent borders |
| Dark Navy (Background/Cards) | `#0f172a` | Navbar, dark theme cards, headers |
| Light Blue / Muted Card BG | `#dbeafe` | Standard cards, table backgrounds, stat blocks |
| Page Background | `#f8fafc` | Main body background |
| White | `#ffffff` | Text, card elements on dark backgrounds |
| Text Muted | `#64748b` | Sub-labels, secondary table details |

## Typography
- **Primary Font**: `'Poppins', sans-serif` for body text, labels, and UI elements.
- **Display Font**: `'Playfair Display', serif` (weights 700 and 900) for major page titles, card names, and large metrics.
- **Headings**: Uppercase, bold/black weight, styled with sharp modern hierarchy.

## Layout Rules
- **Layout & Structure**: Every page extends `_Layout.cshtml`, which provides the standard dark navy navbar and matching footer.
- **Max Content Width**: Container-based layout centered via max-width wrappers (e.g., `.profile-container` max-width 1200px).
- **Cards**: Distinct card styles utilizing either Light Blue (`#dbeafe`) or Dark Navy (`#0f172a`) with sharp or minimal rounded corners and high-contrast typography.
- **Tables**: Clean border-collapse layouts featuring dark navy headers (`#0f172a`), light blue row backgrounds (`#dbeafe`), and clear uppercase text styling.
- **Buttons**: Outlined or solid interactive components transitioning to crimson red (`#b91c1c`) on hover.

## CSS Reference Variables
```css
:root {
  --color-primary: #b91c1c;
  --color-bg-dark: #0f172a;
  --color-card-light: #dbeafe;
  --color-body-bg: #f8fafc;
  --color-text-dark: #0f172a;
  --color-text-muted: #64748b;
  --font-main: 'Poppins', sans-serif;
  --font-display: 'Playfair Display', serif;
}


prompt to use 
"Build a Razor view for [describe the page]. Follow the GymRats dark navy and crimson design system: 
use a light blue card background (#dbeafe) or dark navy (#0f172a), crimson red accents (#b91c1c), 'Playfair Display' for major titles/numbers, 
and 'Poppins' for body text. Keep layouts clean, uppercase structural elements where appropriate, 
and ensure compatibility with the site's _Layout.cshtml."