# FlappyBird2D

Videojoc 2D desenvolupat amb **Unity 6000.3.24f1**. El jugador controla un ocell que avança automàticament, esquiva obstacles mòbils, recull monedes i s'enfronta a dracs xinesos.

## Com executar el projecte

1. Clona el repositori.
2. Afegeix la carpeta del projecte a Unity Hub i obre-la amb la versió indicada de Unity.
3. Obre `Assets/Scenes/MainScene.unity`.
4. Prem **Play**.

**Control:** clic esquerre del ratolí per impulsar l'ocell cap amunt.

## Funcionalitats implementades

### Moviment de l'ocell i càmera

- L'ocell avança automàticament cap a la dreta amb `Rigidbody2D.linearVelocity`.
- Cada clic aplica una força vertical amb `AddForce`.
- La càmera segueix la posició horitzontal de l'ocell a `LateUpdate()`, mantenint l'altura i la profunditat.
- Tocar el terra, el sostre o un obstacle reinicia immediatament l'escena.

**Scripts:** `BirdPhysics.cs` i `CameraScript.cs`.

### Obstacles mòbils

- Es desplacen verticalment amb un `Rigidbody2D` sense gravetat.
- `InvokeRepeating` inverteix la velocitat després d'un interval configurable.
- Cada instància pot tenir velocitat, direcció inicial i interval diferents.
- El terra, el sostre i els obstacles utilitzen la capa `GroundPhysics`, amb les col·lisions entre elements d'aquesta capa desactivades perquè no es frenin entre ells.
- Es reutilitzen mitjançant el prefab `obstacle_0`.

**Script:** `ScriptObstacle.cs`.

### Temps transcorregut

- El cronòmetre acumula segons amb `Time.deltaTime`.
- Un text TextMeshPro mostra el temps amb un decimal.
- Es reinicia quan es recarrega l'escena.

**Script:** `ScriptTimeText.cs`.

### Monedes i puntuació

- Sprite de moneda amb transparència i rotació visual sobre l'eix Y.
- Un `CircleCollider2D` amb `Is Trigger` detecta la recollida sense bloquejar l'ocell.
- La moneda identifica el jugador mitjançant el component `BirdPhysics`.
- Cada moneda crida `ScoreManager.AddPoint()`, suma **1 punt** i es destrueix.
- La puntuació es mostra amb TextMeshPro.
- Cada moneda localitza el gestor amb `FindFirstObjectByType`.
- El prefab `PickupCoin` permet distribuir monedes pel nivell.

**Scripts:** `CoinScript.cs` i `ScoreManager.cs`.

### Comptador d'obstacles superats

- Un trigger vertical independent se situa darrere de cada obstacle, a la seva posició **X + 1**.
- Quan l'ocell el travessa, incrementa el recompte i destrueix el trigger.
- El total es mostra amb TextMeshPro i és independent de la puntuació de les monedes.
- Comptem cada obstacle individualment, no per parelles.
- El prefab `ObstaclePassTrigger` permet reutilitzar la configuració.

**Scripts:** `ObstacleCounter.cs` i `ObstaclePassTrigger.cs`.

### Enemics: dracs xinesos

- Spritesheet de sis poses amb foc decoratiu, importada com a sprite `Multiple` i dividida en sis columnes i una fila.
- Animació `DragonFly.anim` de sis fotogrames a **8 FPS**, amb reproducció en bucle mitjançant un Animator Controller.
- Cada drac troba automàticament l'ocell a través del component `BirdPhysics`.
- S'activa quan el jugador és a **6 unitats** i continua volant cap a l'esquerra a **2 unitats per segon**.
- El desplaçament utilitza `Time.deltaTime` perquè sigui independent dels FPS.
- El prefab `Dragon` té una escala de **(1.5, 1.5, 1)** i un `CapsuleCollider2D` horitzontal amb `Is Trigger`.
- Hi ha tres instàncies de drac per poder provar el sistema de vida.

**Script:** `DragonEnemy.cs`.

### Vida del jugador

- L'ocell comença amb **3 vides**.
- Quan el drac detecta el component `PlayerHealth`, crida `TakeDamage()` i resta una vida.
- El contacte es detecta amb `OnTriggerEnter2D`: mantenir-se solapats no resta vida contínuament. Separar-se i tornar a entrar pot provocar un altre dany.
- La vida restant es mostra al marcador `HealthText` amb TextMeshPro.
- Quan la vida arriba a zero, es recarrega l'escena i es recuperen les tres vides.

**Script:** `PlayerHealth.cs`.

> Els dracs resten vida; els obstacles, el terra i el sostre continuen provocant un reinici immediat, independentment de les vides restants. El foc del drac és només decoratiu.

## Organització del projecte

```text
Assets/
├── Animations/   # Animacions i controladors Animator
├── Prefabs/      # Obstacles, monedes, triggers i dracs reutilitzables
├── Scenes/       # MainScene.unity
├── Scripts/      # Lògica del joc i dels marcadors
└── Sprites/      # Recursos gràfics
```

Els prefabs comparteixen configuració entre instàncies. Els canvis locals de cada instància es conserven com a *overrides*. Els sprites de la moneda i del drac s'han generat amb assistència d'IA i s'han configurat a Unity.

## Comprovacions realitzades

- Recollir diverses monedes incrementa la puntuació d'una en una.
- Els obstacles no recullen monedes quan les travessen.
- Cada trigger d'obstacle incrementa el recompte i desapareix.
- Els dracs s'activen per proximitat i es mouen cap a l'esquerra.
- Tres contactes amb dracs redueixen la vida de 3 a 0 i reinicien la partida.
- El solapament continu amb un drac no aplica dany repetidament.
- En reiniciar, reapareixen els objectes de l'escena, els comptadors tornen a zero i es recuperen les tres vides.

## TODO: millores opcionals

- [ ] Fer la rotació de les monedes independent dels FPS amb `Time.deltaTime`.
- [ ] Separar el sprite giratori de la moneda del seu collider.
- [ ] Ajustar els triggers a X + 1,2 si es vol comptar només quan l'ocell sencer hagi passat.
- [ ] Validar les referències als components i gestors.
- [ ] Evitar intervals d'inversió de moviment iguals o inferiors a zero.
- [ ] Afegir una pantalla de Game Over amb els resultats abans de reiniciar.
