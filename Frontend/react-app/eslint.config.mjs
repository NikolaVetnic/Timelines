import { defineConfig } from "eslint/config";
import globals from "globals";
import js from "@eslint/js";
import pluginReact from "eslint-plugin-react";

export default defineConfig([
  { 
    files: ["**/*.{js,mjs,cjs,jsx}"],
    languageOptions: { 
      globals: globals.browser 
    },
    plugins: { 
      js 
    },
    extends: [
      "js/recommended"
    ],
    rules: {
      "no-control-regex": 0
    }
  },
  pluginReact.configs.flat.recommended,
]);