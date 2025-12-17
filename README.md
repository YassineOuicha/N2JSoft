# Technical-Assessment-Developer-Dotnet-Angular


## Général

Ce TP technique vise à tester vos compétences en Angular et ASP.NET Core. Ne perdez pas de temps sur le design, il ne sera pas évalué.

### Prérequis techniques

Le TP utilise .NET Aspire pour configurer une bdd PostgreSQL, une webapi ASP.NET Core, et un front Angular.

Il vous faudra donc :
- Docker Desktop (ou similaire)
- .NET 9 SDK avec ASP.NET
- nodeJS >= 22.12

### Lancement

Il suffit de lancer le projet Aspire disponible dans `backend\src\AppHost`.

Aspire lance la bdd, le backend et le frontend. 

La console dotnet affiche le lien vers le dashboard Aspire pour consulter les services applicatifs.

### Critères d'évaluation

Nous fournissons ci-dessous les besoins fonctionnels à développer, ainsi que leurs critères d'acceptation.

Pour faciliter notre évaluation, merci de :

- faire des commits réguliers ;
- compléter la section "Mes Notes" à la fin de ce readme.md pour lister les tâches réalisées et éventuelles pistes d'amélioration.

### Rendu du travail

Créer un repo privé sur GitHub et inviter l'utilisateur suivant : n2jsoft-hr-cr

Ce repo doit inclure l'ensemble des fichiers (backend et frontend).


## Besoins fonctionnels

On souhaite mettre en place une solution de gestion des dépenses utilisable par un administrateur (typiquement secrétaire ou comptable) pour des utilisateurs de la société.

Les utilisateurs ne saisissent donc pas directement leurs dépenses ; c'est l'administrateur qui s'en charge.

### Gestion des Users

Un utilisateur doit être affectable à une note de frais ; on dit de cet utilisateur qu'il est l'"utilisateur affecté" à la note de frais.

Il ne peut y avoir qu'un seul utilisateur affecté à une note de frais, mais un utilisateur peut etre affecté à plusieurs notes de frais.

Chaque utilisateur a droit à un nombre maximum de dépenses par mois calendaire.

Un utilisateur peut être actif ou inactif. Seuls les utilisateurs actifs peuvent être affectés à une note de frais.

Un utilisateur a un nom, prénom et une adresse postale.

Un utilisateur peut etre supprimé (suppression logique).

### Gestion des Expenses

Une dépense est définie par les attributs suivants :
- une date au format exemple suivant : "mercredi 15 octobre 2025" ;
- une description : maximum 50 caractères ;
- un montant en euros ;
- une adresse de facturation (enseigne / rue + code postal + ville).

Ces attributs peuvent être éditables en tout temps.

Dans le cas où l'utilisateur a atteint son quota de dépenses au mois, un message d'erreur doit l'en informer et il ne peut pas créer sa nouvelle dépense.

Une dépense peut etre supprimée (suppression logique).

### Gestion des Expense Reports

Une note de frais contient les dépenses d'un utilisateur au mois.

Le format de l'intitulé d'une note de frais doit respecter le format "Utilisateur - Mois Année", par ex : "Juste Leblanc - Octobre 2025".

L'intitulé n'est pas éditable.

Une note de frais peut être supprimable (suppression physique).

La page de visualisation des notes de frais permet d'ajouter une note de frais en sélectionnant le mois et l'utilisateur.

La page de visualisation d'une note de frais permet : 
- d'y ajouter des dépenses ;
- d'afficher les dépenses existantes par lot de 5.


## Mes Notes:

### Architecture & Backend

L’application backend est construite selon une **Clean Architecture (Onion)** afin de garantir une séparation claire des responsabilités :

- **Domain** : entités, value objects, règles métier
- **Application** : use cases, DTOs, interfaces
- **Infrastructure** : EF Core, repositories, persistance
- **WebApi** : contrôleurs HTTP

Utilisation d’**ASP.NET Core Web API** avec contrôleurs plutôt que des endpoints minimaux afin de conserver une structure claire et extensible.

### CQRS

Mise en place du pattern **CQRS**, regroupées actuellement dans les mêmes services applicatifs.

- Piste d’amélioration : séparation stricte **Command / Query** dans des handlers dédiés.

### Entity Framework Core

Entity Framework Core est utilisé pour le mapping et le requêtage de la base de données:

- Configuration explicite des propriétés (types, longueurs, champs requis)
- Relations et clés correctement définies
- Utilisation de navigation properties pour les liens entre entités

### Value Objects (Owned Types)

Utilisation de Value Objects via des **Owned Types** pour les addresses pour la ré-utilisation selon le besoin :

- `PostalAddress` pour représenter l'addresse des utilisateurs
- `BillingAddress` pour represénter l'addresse de la facutation des dépenses

### Suppressions

- Suppression logique pour les utilisateurs et les dépenses
- Suppression physique pour les notes de frais

### Règles métier & gestion des erreurs

Les règles métier critiques sont centralisées côté backend :

- Quota mensuel de dépenses par utilisateur
- Utilisateur actif / inactif
- Validations métier (description, montants, etc.)

Gestion explicite des erreurs via des **DomainErrors**, permettant de retourner des messages clairs et précis au frontend.

---

### Frontend (Angular)

Architecture Angular structurée par **features** et **core/shared**, facilitant la maintenance et l’évolution.

### Angular Material

Utilisation de **Angular Material** pour :

- Tables
- Pagination
- Dialogs
- Form-fields

Cela améliore la lisibilité et accélère le développement sans se focaliser sur le design.

### Composants réutilisables

Mise en place de composants réutilisables :

- Dialogs communs pour la création / édition des utilisateurs et des dépenses
- Blocs fonctionnels isolés (ex : expenses block)

### Formulaires

Gestion des formulaires avec validation obligatoire des champs et règles spécifiques (ex : montant strictement positif).

### Accès API

Utilisation d’un **ApiClient centralisé** pour standardiser les appels HTTP vers l’API :

- GET
- POST
- PUT
- DELETE

### Modèles TypeScript

Utilisation d’interfaces TypeScript pour les modèles (DTOs) afin de :

- Garantir le typage
- Faciliter l’extension future
- Assurer la cohérence entre backend et frontend
- Ent
- Etendre facilement les modèles si besoin

### Gestion des erreurs UI

Gestion des erreurs côté UI via un **SnackbarService** afin de notifier clairement l’administrateur en cas d’erreur métier ou technique.

### Styles & configuration

- Centralisation des styles globaux via `styles.scss`
- Utilisation des variables d’environnement pour standardiser les URLs de l’API

---

### Sécurité / rôles

L’application est conçue comme un outil interne utilisé exclusivement par un administrateur (secrétaire / comptable).

Aucun mécanisme d’authentification ou de gestion de rôles n’a été implémenté volontairement afin de rester dans le périmètre du test technique.

Tous les traitements (création, modification et suppression des utilisateurs, notes de frais et dépenses) sont effectués par l’administrateur pour le compte des utilisateurs.

Une évolution naturelle consisterait donc à ajouter :

- Une authentification JWT / Identity
- Un rôle `Admin` pour représenter l'administrateur
- Des guards côté Angular et des `[Authorize]` côté API

---

### Pistes d’amélioration

- Séparation stricte Command / Query (CQRS avancé): actuellement les services regroupent les deux
- Ajout d’un système d’authentification et de rôles
- Amélioration du thème Material et personnalisation avancée des styles design
- Tests unitaires et tests d’intégration (Pour ce TP Postman est utilisé pour les tests)
- Internationalisation (i18n) pour traduire les interfaces
- Validation plus avancée côté frontend (Reactive Forms)

