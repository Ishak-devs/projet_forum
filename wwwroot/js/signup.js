 //Fonction affichant les matières si l'inscri est un professeur
    function verif_role() {
        const role_saisie = document.querySelector('input[name="Role"]:checked').value;
    const Matiere_saisie = document.getElementById("Matiere_saisie");
    // const Autre_matiere = document.getElementById("Autre_matiere");

    if (role_saisie === 'Professeur') {
        Matiere_saisie.style.display = 'block';
                } else {
        Matiere_saisie.style.display = 'none';
                    // Autre_matiere.style.disaplay = 'none';
                         
                }

            }

    function verif_autre_matiere() {
                          const Matiere_saisie = document.getElementById("Matiere_saisie");
    const Autre_matiere = document.getElementById("Autre_matiere");

    if (Matiere_saisie.value === 'Autre') {
        Autre_matiere_input.style.display = 'block';
                       } else {
        Autre_matiere_input.style.display = 'none';
                       }
                    }
