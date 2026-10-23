# Colector VS 3D — Scripting tradicional vs Visual Scripting en Unity

Juego 3D sencillo de recolección hecho en **Unity 6**. El jugador, una cápsula, se mueve por una arena cerrada y debe recoger 8 monedas.

- **Movimiento:** programado con un script tradicional en **C#**.
- **Recolección de ítems:** programada con **Visual Scripting** (Script Machine).

**Autor:** Leandro Pozo — Universidad de las Américas (UDLA)

---

## Controles

| Tecla | Acción |
|---|---|
| ↑ ↓ ← → | Mover al jugador |

---

## 1. Movimiento con C#

Script: [`Scripts/PlayerMovement.cs`](Scripts/PlayerMovement.cs)

- **Update:** lee las flechas del teclado con `Input.GetAxisRaw` y calcula la dirección normalizada.
- **FixedUpdate:** usa `Vector3.MoveTowards` para acelerar y frenar de forma progresiva, logrando un movimiento suave.
- **MovePosition:** mueve al jugador mediante el `Rigidbody` para que las colisiones y triggers funcionen correctamente.
- **Quaternion.Slerp:** gira al jugador suavemente hacia la dirección en la que se mueve.

Parámetros ajustables desde el Inspector: velocidad máxima, aceleración y velocidad de giro.

---

## 2. Recolección con Visual Scripting

El `Player` tiene una **Script Machine** con el grafo `RecoleccionGraph` y dos variables de grafo:

| Variable | Tipo | Valor inicial |
|---|---|---|
| `puntos` | Float | 0 |
| `total` | Float | 8 |

Las monedas tienen el collider en modo **Is Trigger** y el tag **Item**.

### Grafo completo

![Grafo completo](docs/grafo_completo.png)

### Detección

`On Trigger Enter` → `Compare Tag ("Item")` → `If`

Verifica que el objeto tocado sea una moneda.

![Detección](docs/grafo_deteccion.png)

### Recolección

`Get Game Object` → `Destroy` → `Set puntos = puntos + 1` → `Debug Log (puntos)`

Elimina la moneda, suma un punto y lo muestra en consola.

![Recolección](docs/grafo_recoleccion.png)

### Victoria

`Equal (puntos, total)` → `If` → `Debug Log ("¡Ganaste! Recolectaste todas las monedas")`

![Victoria](docs/grafo_victoria.png)

### Componentes del Player

![Inspector del Player](docs/inspector_player.png)

### Juego funcionando

![Juego y consola](docs/juego_console.png)

---

## 3. Ventajas y desventajas

| Criterio | Scripting tradicional (C#) | Visual Scripting |
|---|---|---|
| **Facilidad de implementación** | Requiere saber programar, pero es rápido y directo para quien ya sabe | Más amigable para principiantes; aunque buscar el nodo correcto puede tomar tiempo |
| **Legibilidad** | Compacto, ordenado y se puede comentar | En lógica pequeña se entiende muy bien visualmente; al crecer se llena de cables |
| **Escalabilidad** | Muy escalable: clases, reutilización y buen manejo con Git | Difícil de mantener en proyectos grandes y de revisar en control de versiones |

**Conclusión:** Visual Scripting es útil para prototipos rápidos o para diseñadores, mientras que C# es mejor para lógica compleja y proyectos que van a crecer. Combinarlos permite aprovechar lo mejor de cada uno.
