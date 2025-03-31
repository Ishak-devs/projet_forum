//Fonction affichant les matières si l'inscri est un professeur
const Matiere = document.getElementById("Matiere");
const Matiere_saisie = document.getElementById("Matiere_saisie");
const Autre_matiere_input = document.getElementById("Autre_matiere_input");

function verif_role() {
    const role_saisie = document.querySelector('input[name="Role"]:checked').value;
    
    if (role_saisie === 'Professeur') {
        Matiere.style.display = 'block';
    } 
    else {
        Matiere.style.display = 'none';
    }
    
     verif_autre_matiere()
}

function verif_autre_matiere() {
    const role_saisie = document.querySelector('input[name="Role"]:checked').value;
    
    if (Matiere_saisie.value === 'Autre' && role_saisie === 'Professeur') {
        Autre_matiere_input.style.display = 'block';
    } 
    else {
        Autre_matiere_input.style.display = 'none';
    }
}