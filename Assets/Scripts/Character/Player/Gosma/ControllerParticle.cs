using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ControllerParticle : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    private ControllerMove cMove = null;

    void OnEnable() {
        cMove = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMove>();
    }

    private void LateUpdate() {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++) {
            //if (m_Particles[i].position.y < cMove.FloorPosition.y - 0.1f)
            //m_Particles[i].remainingLifetime = -1f;

 

        }

        // Apply the particle changes to the Particle System
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded() {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }

}
