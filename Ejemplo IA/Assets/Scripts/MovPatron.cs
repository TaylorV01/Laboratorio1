using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SMovimiento
{
    public float rotacion;
    public float tiempo;
    public float velocidad;
    public float velRotacion;

    public SMovimiento(float pRotacion, float pTiempo, float pVelocidad, float pVelRotacion)
    {
        rotacion = pRotacion;
        tiempo = pTiempo;
        velocidad = pVelocidad;
        velRotacion = pVelRotacion; 
    }
}

public class MovPatron : MonoBehaviour
{
    private int cantidadPasos;
    private List<SMovimiento> patron = new List<SMovimiento>();
    private float tiempo = 0;
    private int indice = 0;
    private Vector3 direccion;

    // Start is called before the first frame update
    void Start()
    {
        // Crear el patron
        patron.Add(new SMovimiento(30, 2, 5, 3));
        patron.Add(new SMovimiento(-30, 2, 5, 2));
        patron.Add(new SMovimiento(0, 3, 5, 0));
        patron.Add(new SMovimiento(0, 2, 2, 0));
        patron.Add(new SMovimiento(15, 5, 0, 5));
        cantidadPasos = patron.Count;
        indice = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo > patron[indice].tiempo)
        {
            // Reseteamos tiempo y avanzamos el movimiento
            tiempo = 0;
            indice++;

            // Verificamos si es necesario repetir el patron
            if (indice >= cantidadPasos)
                indice = 0;
        }

        // Calculamos el vector de rotacion
        direccion = Quaternion.AngleAxis(patron[indice].rotacion, Vector3.up) * transform.forward;
        Quaternion rotObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotObjetivo, patron[indice].velRotacion * Time.deltaTime);
        transform.Translate(transform.forward * patron[indice].velocidad * Time.deltaTime);
    }
}
