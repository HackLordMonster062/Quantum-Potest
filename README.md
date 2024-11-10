# Quantum Potest

_Quantum Potest_ is a video game inspired by Quantum Mechanics, with the goal of exploring the whimsical nature of this scientific field by presenting unique and challenging puzzles derived from real-life observations.

#### Core Features:
- **Excitation**: All particles, including the player, when given energy either by a photon or a device, will enter a state of **excitement**. Each particle behaves differently to excitement, and some react differently to different energy levels. An excited particle will slowly relax its energy level until depletion.
- **Spin**: Some particles have a **spin** property, which defines certain aspects of their behavior. Spin can be either horizontal or vertical, and can be flipped with a specific device.

#### Particles:
- **Photons**: Fly around and hold energy. They will generally vanish when hitting an object, except for specific reflective surfaces. When hitting a particle or the player, the energy will transfer to that particle and excite it.
- **Spectron**: Can be used to open same-colored doors. A key can exist in a superposition of colors, in which case it needs to be collapsed into the correct color using the Polaroid device (more detail in the devices section). Flipping the spin inverts its colors along the color range (1-10).
- **Emitters**: When relaxing from excitation, the emitter will shoot a photon in the direction it's facing. When in the horizontal spin state, the emitter can face one of the cardinal directions, and in the vertical state it can face up or down. It will keep its orientation until rotated by a rotating device or a spin-altering device.
- **Massive Particle**: Big and heavy, creates a force field around it trapping smaller particles in a certain range. Once excited, the particle will temporarily release all trapped particles.
- **Activator Particle**: Once excited, it will trigger nearby logical devices.

#### Devices:
- **Rails**: Can be used to move a specific device over a specific path forward and backward. The exact setup of the rail (path and attached device) is predetermined and cannot be altered.
- **Anchor**: Traps a single particle above it forever. The particle can still be interacted with, but cannot move.
- **Activator anchor**: Traps a single particle above it, which can be picked up. When activated, the anchor will attempt to excite the trapped particle.
- **Rotator**: Will change the orientation of the particle trapped above it.
- **Spin device**: Will flip the spin of the particle trapped above it.
- **Colored door**: Will disappear once a same-colored key is in range.
- **Polaroid**: Can be used to filter the colors of a Spectron based on its _width_ - a filter with _width_ of 2 on a Spectron with the colors 1, 2, and 3, will filter out the color 3, and only leave 1 and 2. 

  It consists of an anchor and an energy receiver. The _width_ of the filter can be tuned by exciting the energy receiver - when excited to a certain energy level, the _width of the polaroid is set to that level. The receiver will relax after a short duration but the polaroid will only update once the receiver is re-excited.

  The Spectron-Polaroid mechanics will act as the objective of most levels and will be introduced progressively.

#### Extra features (May not be included):
- **Layered Realities**: The player's character exists in multiple, slightly different rooms simultaneously. Collisions in one room reflect in all other clones. In order to progress, the player must "prune" all undesired realities to collapse into the correct one.

### Plans and Ideas:
- Making Layered Realities involve other types of interactions rather than just collisions
- Anti-players, which when colliding with the player make the player (or everything around them) move backward through time, meaning all other game mechanics and interactions are flipped. 
For simplicity of logic and understanding this will probably exclude gravity and similar effects


## Disclaimer

_Quantum Potest_ is purely inspired by quantum mechanics and does not aim to mimic real-life physics accurately.
