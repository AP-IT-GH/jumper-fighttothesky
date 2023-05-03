Van: Fleur Van Buijten
# Beschrijving

In dit project heb ik geprobeerd om een machine learning agent te trainen in Unity om verschillende blokken te ontwijken.

# Benodigheden

Om dit project uit te voeren, heb je het volgende nodig: Unity, Python, de mlagent package in Unity, en Anaconda. 

# Omgeving

Ik heb de volgende objecten gecreëerd in de Unity omgeving: één blok dat werkt als agent, twee blokjes als obstakels waar achter een plane is geplaatst om later als beloning aan de agent te kunnen geven, en een plane aan het einde van de omgeving. Natuurlijk heb ik ook een plane gemaakt voor de grond. Vergeet niet om alle blokken, behalve de grond, een Rigidbody te geven. Een Rigidbody maakt het mogelijk dat een object kan bewegen als gevolg van krachten en impulsen en voor de oncollision is dit ook belangrijk.

# Scripting

Ik heb twee scripts geschreven: één voor de obstakels en één voor de agent.

## Obstacle script

In het Obstacle script heb ik beschreven hoe de blokjes bewegen door middel van een kracht (force) en dat ze moeten respawnen als ze de agent raken of het einde van de speelomgeving bereiken. Het respawnen van de obstakels zorgt ervoor dat de agent kan blijven trainen en niet vastloopt in een situatie waarbij er geen obstakels meer zijn om te ontwijken.

## mlagent script

In het mlagent script heb ik de logica geschreven voor de agent. De agent krijgt punten als hij voorbij het obstakel komt en de muur daarna correct aanraakt. Natuurlijk krijgt hij ook minpunten als hij het obstakel raakt en dan stopt de episode. In dit script zijn ook de parameters van de agent geconfigureerd.

# Instellen van parameters

Daarna heb ik het mlagent script aan de agent gegeven, samen met het Decision Requester script dat in Unity zit. Ik heb de lege parameters in het mlagent script ingevuld met de juiste waarden en eventueel nog andere parameters aangepast die beter bij de omgeving passen. Daarna heb ik het obstacle script aan de obstakels gegeven.

# Training

Ik ben dan begonnen met het effectief trainen van de ML-agent en heb gemerkt dat het een uitdagend proces was. Hoewel de prestaties van de agent aanvankelijk laag waren, lijkt het erop dat hij in staat was om veel te leren tijdens het trainingsproces. Ondanks de lage punten die de agent behaalde, heb ik geprobeerd om de training te optimaliseren door het aanpassen van verschillende parameters en het herhaaldelijk uitvoeren van de training. Ik heb geleerd dat het trainen van een ML-agent een iteratief proces is dat veel tijd en geduld vereist om succesvol te zijn.

![alt text](https://github.com/AP-IT-GH/jumper-assignment-fighttothesky/blob/agent/agent.PNG?raw=true)
