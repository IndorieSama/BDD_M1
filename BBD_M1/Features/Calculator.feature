Feature: Calculatrice
    Scenario Outline: Addition de plusieurs nombres
        Given la calculatrice est initialisée
        When je calcule la somme de <nombres>
        Then le résultat doit être <résultat>

        Examples:
          | nombres | résultat |
          | 2,3     | 5        |
          | 1,2,3   | 6        |
          | 10,0,-2 | 8        |

    Scenario Outline: Soustraction de plusieurs nombres
        Given la calculatrice est initialisée
        When je calcule la soustraction de <nombres>
        Then le résultat doit être <résultat>

        Examples:
          | nombres | résultat |
          | 10,3    | 7        |
          | 20,5,5  | 10       |
          | 5,10    | -5       |

    Scenario Outline: Multiplication de plusieurs nombres
        Given la calculatrice est initialisée
        When je calcule le produit de <nombres>
        Then le résultat doit être <résultat>

        Examples:
          | nombres | résultat |
          | 2,3     | 6        |
          | 1,2,3   | 6        |
          | 4,0     | 0        |

    Scenario Outline: Division de plusieurs nombres
        Given la calculatrice est initialisée
        When je calcule la division de <nombres>
        Then le résultat doit être <résultat>

        Examples:
          | nombres | résultat |
          | 10,2    | 5        |
          | 100,2,5 | 10       |

    Scenario: Division par zéro
        Given la calculatrice est initialisée
        When je calcule la division de 10,0
        Then une erreur de division par zéro est levée

    Scenario: Calcul séquentiel avec plusieurs opérations
        Given la calculatrice est initialisée
        When je calcule "10 * 2 + 5 - 4"
        Then le résultat doit être 21
        