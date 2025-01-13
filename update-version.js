const fs = require('fs');
const path = require('path');

const version = process.env.NEW_VERSION;

if (!version) {
    console.error('NEW_VERSION environment variable is not set.');
    process.exit(1);
}

const projectPath = 'Directory.Build.props'; // Cible uniquement ce fichier
const fullPath = path.join(__dirname, projectPath);

// Lire le contenu du fichier
const content = fs.readFileSync(fullPath, 'utf8');

// Mettre à jour la version
const updatedContent = content.replace(
    /<Version>.*<\/Version>/,
    `<Version>${version}</Version>`
);

// Écrire le contenu mis à jour dans le fichier
fs.writeFileSync(fullPath, updatedContent, 'utf8');
console.log(`Updated ${projectPath} to version ${version}`);