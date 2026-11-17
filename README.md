# FlappyBird2D

Videojoc 2D desenvolupat amb **Unity 6000.3.24f1**. Controla un ocell, esquiva obstacles mòbils i dracs xinesos, recull monedes i recupera vida amb préssecs màgics en un recorregut infinit.

## Jugar al navegador

**[Obre el joc a GitHub Pages](https://imartinez-dam.github.io/FlappyBirds2D/)**

Prem **Jugar** a la pantalla inicial per començar. Si el teclat no respon, fes clic dins de la zona del joc per donar-li el focus.

## Instruccions del joc

### Controls

| Acció | Control |
|---|---|
| Aletejar i pujar | Clic esquerre, fletxa amunt o **W** |
| Accelerar cap a la dreta | Mantenir fletxa dreta o **D** |
| Frenar | Mantenir fletxa esquerra o **A** |
| Caure | Deixar actuar la gravetat; no cal prémer cap tecla |

Cada pulsació d'aleteig aplica un impuls cap amunt; mantenir la tecla premuda no produeix aleteigs continus. L'ocell avança automàticament: amb la configuració actual, la velocitat és de **2 unitats/segon**, augmenta a **4** en accelerar i baixa a **0** en frenar. Si prems les dues direccions alhora, es manté l'avanç normal.

### Objectiu i elements

Intenta sobreviure tant com puguis, recollir monedes i superar obstacles:

- **Moneda daurada:** suma **1 punt** i desapareix en recollir-la.
- **Préssec màgic:** recupera **1 vida**, fins al màxim de **3**. Si ja tens la vida completa, no es consumeix.
- **Obstacles de fusta:** es mouen verticalment. Tocar-ne un acaba la partida immediatament, encara que quedin vides.
- **Dracs:** comencen a volar cap a l'esquerra quan t'apropes. Cada entrada en contacte resta **1 vida**. El foc és decoratiu.
- **Terra i sostre:** tocar-los també acaba la partida immediatament.

Comences amb **3 vides**. Mantenir-te solapat amb un drac no resta vida contínuament, però separar-te i tornar-lo a tocar pot provocar un altre dany. Quan les vides arriben a zero, apareix el Game Over.

Els marcadors mostren les monedes, els obstacles superats, la vida restant i el temps en format **minuts:segons**.

### Menús

- **Jugar:** inicia una partida nova.
- **Game Over:** pausa la partida i mostra monedes, obstacles superats i temps final.
- **Reiniciar:** comença una partida nova directament, amb els comptadors a zero i les tres vides recuperades.
- **Sortir:** torna a la pantalla inicial amb el joc aturat; no tanca la pestanya del navegador.

Els controls de l'ocell i la rotació de les monedes queden bloquejats durant els menús.

## Executar el projecte a Unity

1. Clona el repositori.
2. Afegeix la carpeta del projecte a Unity Hub i obre-la amb **Unity 6000.3.24f1**.
3. Obre `Assets/Scenes/MainScene.unity`.
4. Prem **Play** a Unity i després **Jugar** al menú del joc.

## Funcionalitats i implementació

### Ocell i càmera

`BirdPhysics.cs` gestiona els controls, el moviment amb `Rigidbody2D`, la vida, la curació i les peticions de Game Over. L'entrada horitzontal es llegeix a `Update()` i s'aplica a `FixedUpdate()`, conservant la velocitat vertical. `CameraScript.cs` segueix l'ocell horitzontalment a `LateUpdate()`.

### Recorregut infinit i generació

- `LoopingGround.cs` recicla dos trams de terra i dos de sostre quan queden completament fora de la vista per l'esquerra.
- `LevelSpawner.cs` genera recompenses, dracs i obstacles davant de la càmera, amb altures i separacions aleatòries dins de límits configurats.
- Monedes i préssecs comparteixen una seqüència de generació; la probabilitat inicial de préssec és del **15 %**. Dracs i obstacles tenen seqüències independents.
- Els obstacles es generen aleatòriament com a superiors o inferiors, amb la direcció inicial corresponent.
- `CleanupZone.cs`, en una zona trigger darrere de la càmera, elimina els objectes passats segons els tags `coin`, `peach`, `dragon` i `obstacle`. El terra i el sostre es reciclen, no es destrueixen.

### Obstacles i recompte

`ScriptObstacle.cs` mou els obstacles verticalment sense gravetat i inverteix periòdicament la velocitat. Els superiors tenen un interval inicial configurat d'un segon per limitar quant baixen.

Cada obstacle té **dos Box Collider 2D al mateix GameObject**: un físic i un trigger vertical amb offset X = 1 per comptar el pas. Un indicador intern impedeix comptar-lo més d'una vegada. `ObstacleCounter.cs` guarda el total i actualitza el marcador.

El terra, el sostre i els obstacles comparteixen la capa `GroundPhysics`, amb les col·lisions entre aquesta capa desactivades perquè no es frenin entre ells.

### Pickups i puntuació

`Pickups.cs` comparteix la lògica de monedes i préssecs, diferenciats pels tags `coin` i `peach`. Identifica l'ocell amb el tag `Player` i consulta `BirdPhysics` per aplicar la curació.

La rotació és opcional per prefab, utilitza `Time.deltaTime` i només s'executa durant la partida. En recollir una moneda, `Pickups.cs` crida `GameManager.instance.AddPoint()`. El `GameManager` guarda la puntuació, actualitza el marcador i reprodueix el so de recollida des d'un objecte que no es destrueix amb la moneda. Aquesta lògica està integrada al gestor de partida, sense un script `ScoreManager` separat.

### Dracs i animació

`DragonEnemy.cs` troba el jugador, s'activa per proximitat i continua volant cap a l'esquerra. La configuració del prefab utilitza una distància d'activació de **6 unitats**, velocitat de **2 unitats/segon**, escala **(1.5, 1.5, 1)** i un `CapsuleCollider2D` horitzontal amb `Is Trigger`.

`DragonFly.anim` reprodueix una spritesheet de sis fotogrames a **8 FPS**, en bucle mitjançant un Animator Controller.

### Temps, menús i resultats

`ScriptTimeText.cs` acumula temps amb `Time.deltaTime` i el mostra en minuts i segons. `GameManager.cs` gestiona els estats de partida, els panells i els botons. La pausa utilitza `Time.timeScale = 0`, juntament amb el bloqueig explícit dels controls.

El gestor persisteix entre recàrregues amb `DontDestroyOnLoad`, elimina duplicats i reconnecta les referències de la interfície de cada escena, inclòs el marcador de puntuació. En morir, combina la puntuació que guarda amb els comptadors d'obstacles i temps de l'escena per omplir el resum del Game Over. En començar una partida nova, `RestartGame()` posa explícitament la puntuació a zero; la vida, el temps i el recompte d'obstacles es reinicialitzen amb els components de l'escena.

### So

Els efectes utilitzen components `AudioSource`:

- `flap.mp3`: aleteig, des de l'ocell amb `AudioSource.Play()`.
- `coin.mp3`: recollida, des del `GameManager` amb `AudioSource.PlayOneShot(coinSound)`.
- `gameOver.mp3`: derrota, des del mateix reproductor del `GameManager` amb `AudioSource.PlayOneShot(gameOverSound)`.

El reproductor del gestor comparteix els dos efectes mitjançant referències `AudioClip`, sense necessitat de tenir un clip fix assignat a l'AudioSource. En prémer Reiniciar o Jugar, `RestartGame()` atura els sons amb `AudioSource.Stop()` abans de recarregar l'escena.

## Organització del projecte

```text
Assets/
├── Animations/   # Animacions i controladors Animator
├── Prefabs/      # Obstacles, monedes, préssecs i dracs reutilitzables
├── Scenes/       # MainScene.unity
├── Scripts/      # Lògica del joc, generació, neteja i interfície
├── Sounds/       # Efectes d'àudio
└── Sprites/      # Recursos gràfics
docs/            # Build Web publicat a GitHub Pages
```

Els prefabs comparteixen configuració entre instàncies. Els sprites de la moneda, el préssec i el drac s'han generat amb assistència d'IA i s'han configurat a Unity.

## Publicació Web

El build es genera des de **File → Build Profiles → Web**, amb `MainScene` inclosa. Per a la publicació actual a GitHub Pages, s'utilitza **Compression Format → Disabled**.

El contingut complet del build es publica a `docs`, amb `index.html` directament dins d'aquesta carpeta i un fitxer `.nojekyll`. GitHub Pages es configura per publicar la carpeta `/docs` de la branca corresponent.

Per provar un build localment, utilitza **Build And Run**, no obris `index.html` directament des del sistema de fitxers. Si després d'actualitzar Pages es carrega una versió antiga o apareixen errors, comprova que el desplegament hagi acabat i prova una finestra privada o esborra les dades del lloc.

## Comprovacions realitzades

- Controls de ratolí i teclat, acceleració i frenada.
- Recollida de monedes amb puntuació i so.
- Curació limitada a la vida màxima i conservació del préssec quan no cal curar.
- Dany dels dracs, sense repetició contínua durant el solapament.
- Obstacles superiors i inferiors, recompte únic i neteja posterior.
- Reciclatge del terra i del sostre.
- Rotació de monedes aturada durant els menús.
- Menú inicial, Game Over, resultats i diversos reinicis consecutius.
- Reinicialització dels comptadors i de la vida en una partida nova.
- Prova completa després de les simplificacions dels scripts.
