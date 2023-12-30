$(init);

function init(){

  const nom = $('#nom');
  const prenom = $('#prenom');
  const age = $('#age');
  const classe = $('#classe');
  const etablissement = $('#etablissement');
  const enseignant = $('#enseignant');

  const select11 = $('#select11');
  const select12 = $('#select12');
  const select13 = $('#select13');

  const select21 = $('#select21');
  const select22 = $('#select22');
  const select23 = $('#select23');

  const select31 = $('#select31');
  const select32 = $('#select32');
  const select33 = $('#select33');

  const select41 = $('#select41');
  const select42 = $('#select42');
  const select43 = $('#select43');

  const select51 = $('#select51');
  const select52 = $('#select52');
  const select53 = $('#select53');

  const select61 = $('#select61');
  const select62 = $('#select62');
  const select63 = $('#select63');


  /*nomval = nom.val();
    prenomval = prenom.val();
    ageval= age.val();
    classeval = classe.val();
    etablissementval = etablissement.val();
    enseignantval = enseignant.val();*/


    /*select11val = select11.val();
    select12val = select12.val();
    select13val = select13.val();

    select21val = select21.val();
    select22val = select22.val();
    select23val = select23.val();

    select31val = select31.val();
    select32val = select32.val();
    select33val = select33.val();

    select41val = select41.val();
    select42val = select42.val();
    select43val = select43.val();

    select51val = select51.val();
    select52val = select52.val();
    select53val = select53.val();

    select61val = select61.val();
    select62val = select62.val();
    select63val = select63.val();*/

  /*const bouton1 = $('#enregistrer');
  bouton1.click(function(){
    bdd(select11val, select12val, select13val);
    bdd(select21val, select22val, select23val);
    bdd(select31val, select32val, select33val);
    bdd(select41val, select42val, select43val);
    bdd(select51val, select52val, select53val);
    bdd(select61val, select62val, select63val);
  });*/

  
}




fetch('./csvjson.json')
  .then(response => response.json())
  .then(data => {

    listeDeroulante(select11, select12, select13);
    listeDeroulante(select21, select22, select23);
    listeDeroulante(select31, select32, select33);
    listeDeroulante(select41, select42, select43);
    listeDeroulante(select51, select52, select53);
    listeDeroulante(select61, select62, select63);


    function listeDeroulante(groupe, sousGroupe, Aliment){
      const groups = [...new Set(data.map(item => item.alim_grp_nom_fr))];
      groups.forEach(group => {
        const option = document.createElement('option');
        option.value = group;
        option.text = group;
        groupe.add(option);
      });
      
      //Mise à jour de la liste déroulante des sous-groupes
      groupe.addEventListener('change', () => {
        const selectedGroup = groupe.value;
        sousGroupe.innerHTML = '';
        const subgroups = [...new Set(data.filter(item => item.alim_grp_nom_fr === selectedGroup).map(item => item.alim_ssgrp_nom_fr))];
        subgroups.forEach(subgroup => {
          const option = document.createElement('option');
          option.value = subgroup;
          option.text = subgroup;
          sousGroupe.add(option);
        });
      });

      //Mise à jour de la liste déroulante des aliments
      sousGroupe.addEventListener('change', () => {
        const selectedGroupGroup = sousGroupe.value;
        Aliment.innerHTML = '';
        const subsubgroups = [...new Set(data.filter(item => item.alim_ssgrp_nom_fr === selectedGroupGroup).map(item => item.alim_nom_fr))];
        subsubgroups.forEach(subsubgroup => {
          const option = document.createElement('option');
          option.value = subsubgroup;
          option.text = subsubgroup;
          Aliment.add(option);
        });
      });
    }
  })
  .catch(error => console.error(error));

  /*function bdd(groupe, sousgroupe, aliment){

    $.ajax({
      url: 'sondage.php',
      type: 'POST',
      data: {nom: nomval, prenom: prenomval, ageval: age, classeval: classe, etablissementval: etablissement, enseignantval: enseignant, groupe: groupe, sousgroupe: sousgroupe, aliment: aliment},
      success: function() {
          alert("Merci de ta reponse");
      },
      error: function() {
          alert("Ca n'a pas marché");
      }
  });
  }*/