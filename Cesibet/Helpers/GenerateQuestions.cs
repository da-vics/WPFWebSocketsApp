using Cesibet.Models;
using System.Collections.Generic;

namespace Cesibet.Helpers
{
    class GenerateQuestions
    {
        public List<QuestionsModel> Questions { get; set; }
        public GenerateQuestions()
        {
            loadQuestions();
        }

        private void loadQuestions()
        {
            Questions = new List<QuestionsModel>();
            Questions.Add(new QuestionsModel { Question = "Georges Seurat est le premier peintre connu à avoir peint la tour Eiffel", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Quelques minutes environ après le big bang, il y avait plus de protons que de neutrons", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "On doit le « débogage » à l’informaticienne Grace Hopper", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Le cerveau humain produit assez d’électricité pour allumer une petite ampoule", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "La Lune va finir par tomber sur la Terre à cause de la gravité", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "Le rugby est apparu lorsqu’un étudiant pratiquant le football a attrapé le ballon et couru vers le but de l’équipe adverse", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "La pétanque est originaire de la France", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "Les athlètes grecs de l’Antiquité participaient aux compétitions nus", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Les cinq postes au basketball sont meneur, arrière, ailier gauche, ailier droit et pivot", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "Le Canada a été le premier pays à légaliser les unions entre personnes d’un même sexe", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "Les plus importantes ressources géothermiques sont surtout situées près des jonctions des plaques tectoniques", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Dacca est la ville la plus densément peuplée au monde", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "D’une superficie de 105 km2, Paris est l’une des plus petites capitales européennes", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Dans les contes Les mille et une nuits, Sinbad le marin fait trois voyages", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "Venom a été créé par un fan de Marvel", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "À l’origine, Hulk était gris", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Avec l’anglais, le français est la seule langue présente sur les 5 continents", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Au Québec, en Suisse et en Belgique, le mot «wagon» se prononce généralement «ouagon», à l’anglaise", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "Le coyote, comme le loup gris, vit en meute", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "Alexandre va m'attribuer un A pour ce travail", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Les dauphins se fient beaucoup à leur odorat", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Certains requins doivent continuer à nager pendant qu’ils se reposent, car ils ont besoin d’un apport en oxygène constant", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Les primates sont reconnus pour utiliser les relations sexuelles afin de régler leurs problèmes", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Sirius Black et Nymphadora Tonks sont cousins", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "La devise de l’école de Poudlard est «Draco dormiens nunquam titillandus»", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Pré-au-lard est le seul village de Grande-Bretagne à n’avoir que des sorciers pour habitants", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Dans Guardians of the Galaxy Vol. 2, bébé Groot est le fils du Groot original", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "C'est dans un quartier de Lille qu'ont été écrites les paroles de la Marseillaise", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "La ville de Lille a été rattachée à la France en 1767", Answer = "No" });
            Questions.Add(new QuestionsModel { Question = "On peut visiter à Lille la maison natale de Charles de Gaulle", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "Parmi les rue de Lille, on peut trouver la rue du Chat bossu", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "CESI est présente dans deux villes en Algérie", Answer = "Yes" });
            Questions.Add(new QuestionsModel { Question = "CESI dispose de 28 centres en France", Answer = "No" });
        }

    }
}
