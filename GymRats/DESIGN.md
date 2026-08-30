# GymRats — Design System

Paste this whole file into whatever AI tool you're using to generate a view, so every page comes out looking like it belongs to the same app. Keep it simple — this isn't meant to be fancy, just consistent.

## Colors
| Name | Hex | Use for |
|---|---|---|
| Primary (Red) | `#E63946` | Buttons, links, highlights, nav bar |
| Dark | `#1D1D1D` | Headings, text |
| Light Gray | `#F5F5F5` | Page background |
| White | `#FFFFFF` | Cards, form backgrounds |
| Success Green | `#2A9D8F` | "Booked!" confirmations |
| Danger | `#D62828` | Cancel/delete buttons, error messages |

## Typography
- Font: `'Poppins', sans-serif` (import from Google Fonts) — falls back to the Bootstrap default if not loaded.
- Headings: bold, Dark color.
- Body text: regular weight, Dark color, 16px base size.

## Layout Rules
- Every page extends `_Layout.cshtml` — never build a page without `@{ Layout = "_Layout"; }` (or just don't touch that line, Razor does it automatically).
- Max content width: 960px, centered.
- Cards (for classes, bookings): white background, rounded corners (`border-radius: 8px`), light shadow.
- Buttons: rounded (`border-radius: 6px`), Primary color background, white text.
- Forms: stack labels above inputs, full-width inputs, one column (no side-by-side fields).

## CSS to drop into `wwwroot/css/site.css`
This defines the variables above so every view can just reference them.

```css
:root {
  --color-primary: #E63946;
  --color-dark: #1D1D1D;
  --color-bg: #F5F5F5;
  --color-white: #FFFFFF;
  --color-success: #2A9D8F;
  --color-danger: #D62828;
  --font-main: 'Poppins', sans-serif;
}

body {
  background-color: var(--color-bg);
  color: var(--color-dark);
  font-family: var(--font-main);
}

.btn-primary {
  background-color: var(--color-primary) !important;
  border-color: var(--color-primary) !important;
  border-radius: 6px;
}

.btn-danger {
  background-color: var(--color-danger) !important;
  border-color: var(--color-danger) !important;
  border-radius: 6px;
}

.card {
  background-color: var(--color-white);
  border-radius: 8px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.08);
  border: none;
}
```

## Prompt template for AI-generated views
When asking an AI to build a `.cshtml` view, paste this:

> "Build a Razor view for [describe the page]. Use Bootstrap classes for layout, but follow this color palette: primary red `#E63946`, dark text `#1D1D1D`, light gray background `#F5F5F5`. Buttons should use the `.btn-primary` or `.btn-danger` classes already defined in site.css — don't inline new colors. Wrap content in a `.card` div. Keep it simple, one column, no sidebars."

That keeps every generated page visually consistent even if three different people (or three different AI chats) built them.
