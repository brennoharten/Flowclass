import type { Config } from "tailwindcss";

const config: Config = {
	content: [
		"./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
		"./src/components/**/*.{js,ts,jsx,tsx,mdx}",
		"./src/app/**/*.{js,ts,jsx,tsx,mdx}",
	],
	theme: {
		container:{
			center:true,
			padding:"15px",
		},
		screens: {
			sm: "640px",
			md: "768px",
			lg: "960px",
			xl: "1200px",

		},
		fontFamily: {
			primary: "var(--font-jetbrainsMono)",

		},
		extend: {
			colors: {
				primary: "#0F1A42",
				accent: {
					DEFAULT: "#e58f2b",  
					hover: "#FF9966",  
				},
				green: "#1f4c55",
				background: "var(--background)",
				foreground: "var(--foreground)",
			},
			borderRadius: {
				/* lg: "var(--radius)",
				md: "calc(var(--radius) - 2px)",
				sm: "calc(var(--radius) - 4px)", */
			},
			keyframes: {
				"accordion-down": {
					from: { height: "0"},
					to: { heigth: "var(--radix-accordion-content-heigth"}
				},
				"accordion-up": {
					from: { heigth: "var(--radix-accordion-content-heigth"},
					to: { height: "0"},
				}
			},
			animation: {
				"accordion-down" : "accordion-down 0.2s ease-out",
				"accordion-up" : "accordion-up 0.2s ease-out"
			}
		},
	},
	//plugins: [require("tailwindcss-animate")],
};
export default config;
