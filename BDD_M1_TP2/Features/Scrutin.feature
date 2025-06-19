Feature: Calcul du vainqueur au premier tour

    Scenario: Un candidat obtient plus de 50% des voix au premier tour
        Given un scrutin clôturé avec les votes suivants :
          | Candidat | Votes |
          | Alice    | 60    |
          | Bob      | 30    |
          | Claire   | 10    |
        When je calcule le résultat du scrutin
        Then le vainqueur est "Alice"
        And les résultats sont :
          | Candidat | Votes | Pourcentage |
          | Alice    | 60    | 60%         |
          | Bob      | 30    | 30%         |
          | Claire   | 10    | 10%         |
          
    Scenario: Aucun candidat n’obtient plus de 50%, second tour nécessaire
        Given un scrutin clôturé avec les votes suivants :
          | Candidat | Votes |
          | Alice    | 40    |
          | Bob      | 35    |
          | Claire   | 25    |
        When je calcule le résultat du scrutin
        Then aucun vainqueur n’est désigné
        And un second tour est lancé entre :
          | Candidat |
          | Alice    |
          | Bob      |

    Scenario: Second tour désigne un vainqueur
      Given un deuxième tour entre :
        | Candidat | Votes |
        | Alice    | 45    |
        | Bob      | 55    |
      When je calcule le résultat du scrutin
      Then le vainqueur est "Bob"
      
  Scenario: Égalité parfaite au deuxième tour
    Given un deuxième tour entre :
      | Candidat | Votes |
      | Alice    | 50    |
      | Bob      | 50    |
    When je calcule le résultat du scrutin
    Then aucun vainqueur ne peut être déterminé

  Scenario: Égalité entre le 2e et le 3e candidat au premier tour
    Given un scrutin clôturé avec les votes suivants :
      | Candidat | Votes |
      | Alice    | 40    |
      | Bob      | 30    |
      | Claire   | 30    |
    When je calcule le résultat du scrutin
    Then aucun vainqueur n’est désigné
    And un second tour est lancé entre :
      | Candidat |
      | Alice    |
      | Bob      |
      | Claire   |

  Scenario: Vote blanc empêche un candidat d’atteindre 50%
    Given un scrutin clôturé avec les votes suivants :
      | Candidat | Votes |
      | Alice    | 49    |
      | Bob      | 30    |
      | Blanc    | 21    |
    When je calcule le résultat du scrutin
    Then aucun vainqueur n’est désigné
    And un second tour est lancé entre :
      | Candidat |
      | Alice    |
      | Bob      |
