
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfiguracionUI : MonoBehaviour
{
    private Texture2D fondo;
    private Texture2D btnIdioma;
    private Texture2D btnSonido;
    private Texture2D btnControles;
    private Texture2D btnPantalla;
    private Texture2D btnVolver;
    private bool mostrarControles = false;
    private Vector2 scrollInstrucciones = Vector2.zero;
    private Vector2 scrollControles = Vector2.zero;

    private GUIStyle botonInvisible;
    private bool mostrarIdiomas = false;
    private string idiomaSeleccionado = "Español";

    private Texture2D fondoInstrucciones;
    private GUIStyle estiloTituloAyuda;
    private GUIStyle estiloTextoAyuda;
    private GUIStyle estiloCerrarAyuda;


    private void Start()
    {
        // Cargar el fondo
        fondo = Resources.Load<Texture2D>("Configuracion");

        // Cargar los botones
        btnIdioma = Resources.Load<Texture2D>("Icons/BtnIdioma");
        btnSonido = Resources.Load<Texture2D>("Icons/BtnSonido");
        btnControles = Resources.Load<Texture2D>("Icons/BtnControles");
        btnPantalla = Resources.Load<Texture2D>("Icons/BtnPantalla");
        btnVolver = Resources.Load<Texture2D>("Icons/BtnVolver");

        // Cargar el fondo de las instrucciones
        fondoInstrucciones = Resources.Load<Texture2D>("Fondos/Fondo3");

        // No crear estilos gráficos aquí.
        // Se inicializarán dentro de OnGUI().
    }


    private bool estilosAyudaInicializados = false;

    private void InicializarEstilosAyuda()
    {
        if (estilosAyudaInicializados)
            return;

        estiloTituloAyuda = new GUIStyle(GUI.skin.label);
        estiloTituloAyuda.fontSize = 25;
        estiloTituloAyuda.fontStyle = FontStyle.Bold;
        estiloTituloAyuda.alignment = TextAnchor.MiddleCenter;
        estiloTituloAyuda.normal.textColor = Color.white;
        estiloTituloAyuda.wordWrap = true;

        estiloTextoAyuda = new GUIStyle(GUI.skin.label);
        estiloTextoAyuda.fontSize = 17;
        estiloTextoAyuda.fontStyle = FontStyle.Bold;
        estiloTextoAyuda.normal.textColor = Color.white;
        estiloTextoAyuda.wordWrap = true;
        estiloTextoAyuda.richText = true;

        estiloCerrarAyuda = new GUIStyle(GUI.skin.button);
        estiloCerrarAyuda.fontSize = 18;
        estiloCerrarAyuda.fontStyle = FontStyle.Bold;

        botonInvisible = new GUIStyle();
        botonInvisible.normal.background = null;
        botonInvisible.hover.background = null;
        botonInvisible.active.background = null;
        botonInvisible.focused.background = null;

        estilosAyudaInicializados = true;
    }


    private void OnGUI()
    {
        InicializarEstilosAyuda();
        GUI.depth = 0;

        // Dibujar el fondo
        if (fondo != null)
        {
            GUI.DrawTexture(
                new Rect(0, 0, Screen.width, Screen.height),
                fondo,
                ScaleMode.StretchToFill
            );
        }
        else
        {
            GUI.Label(
                new Rect(20, 20, 500, 40),
                "No se encontró Configuracion.jpeg en Resources."
            );
        }

        if (mostrarControles)
        {
            DibujarVentanaControles();
            return;
        }

        if (mostrarIdiomas)
        {
            DibujarVentanaIdiomas();
            return;
        }

        // Tamaño y posición de los botones

        float ancho = Mathf.Min(680f, Screen.width * 0.82f);
        float alto = ancho * 0.24f;
        float espacio = -30f;

        float x = (Screen.width - ancho) / 2f;

        // Distribuir los cinco botones y centrar el conjunto
        float altoTotal = alto * 5f + espacio * 4f;
        float y = (Screen.height - altoTotal) / 2f + 150f;


        DibujarBoton(btnIdioma, "IDIOMA", x, y, ancho, alto);
        DibujarBoton(btnSonido, "SONIDO", x, y + alto + espacio, ancho, alto);
        DibujarBoton(btnControles, "CONTROLES", x, y + (alto + espacio) * 2, ancho, alto);
        DibujarBoton(btnPantalla, "PANTALLA", x, y + (alto + espacio) * 3, ancho, alto);
        DibujarBoton(btnVolver, "VOLVER", x, y + (alto + espacio) * 4, ancho, alto);
        DibujarVentanaControles();


    }

    private void DibujarBoton(
        Texture2D imagen,
        string nombre,
        float x,
        float y,
        float ancho,
        float alto)
    {
        Rect area = new Rect(x, y, ancho, alto);

        // Mostrar la imagen original
        if (imagen != null)
        {
            GUI.DrawTexture(area, imagen, ScaleMode.StretchToFill, true);
        }
        else
        {
            GUI.Label(area, "Falta imagen: " + nombre);
        }

        // Detectar el clic sin dibujar otro botón encima

        if (GUI.Button(area, GUIContent.none, botonInvisible))
        {
            if (nombre == "SONIDO")
            {
                AudioListener.pause = !AudioListener.pause;

                Debug.Log(
                    AudioListener.pause
                        ? "Sonido desactivado."
                        : "Sonido activado."
                );
            }

            else if (nombre == "CONTROLES")
            {
                mostrarControles = true;
            }
            else if (nombre == "PANTALLA")
            {
                Screen.fullScreen = !Screen.fullScreen;

                Debug.Log(
                    Screen.fullScreen
                        ? "Pantalla completa activada."
                        : "Modo ventana activado."
                );
            }
            else if (nombre == "IDIOMA")
            {
                mostrarIdiomas = true;
            }
            else if (nombre == "VOLVER")
            {
                SceneManager.LoadScene("MenuPrincipal");
            }
            else
            {
                Debug.Log("Seleccionaste: " + nombre);
            }

        }

    }



    private void DibujarVentanaControles()
    {
        if (!mostrarControles)
            return;

        float ancho = Mathf.Min(900f, Screen.width - 30f);
        float alto = Mathf.Min(720f, Screen.height - 30f);

        Rect ventana = new Rect(
            (Screen.width - ancho) / 2f,
            (Screen.height - alto) / 2f,
            ancho,
            alto
        );

        // Dibujar el fondo
        if (fondoInstrucciones != null)
        {
            GUI.DrawTexture(
                ventana,
                fondoInstrucciones,
                ScaleMode.StretchToFill
            );
        }

        // Panel oscuro para que el texto contraste
        Color anterior = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.88f);
        GUI.DrawTexture(ventana, Texture2D.whiteTexture);
        GUI.color = anterior;

        // Estilos locales con texto grande y blanco
        GUIStyle titulo = new GUIStyle(GUI.skin.label);
        titulo.fontSize = 26;
        titulo.fontStyle = FontStyle.Bold;
        titulo.alignment = TextAnchor.MiddleCenter;
        titulo.normal.textColor = Color.white;
        titulo.wordWrap = true;

        GUIStyle texto = new GUIStyle(GUI.skin.label);
        texto.fontSize = 19;
        texto.fontStyle = FontStyle.Bold;
        texto.normal.textColor = Color.white;
        texto.wordWrap = true;
        texto.richText = true;

        GUIStyle boton = new GUIStyle(GUI.skin.button);
        boton.fontSize = 20;
        boton.fontStyle = FontStyle.Bold;

        GUILayout.BeginArea(new Rect(
            ventana.x + 25f,
            ventana.y + 15f,
            ventana.width - 50f,
            ventana.height - 30f
        ));

        try
        {
            GUILayout.Label(
                "AYUDA — INSTRUCCIONES DEL JUEGO",
                titulo,
                GUILayout.Height(45f)
            );

            GUILayout.Space(10f);

            scrollInstrucciones = GUILayout.BeginScrollView(
                scrollInstrucciones,
                true,
                true,
                GUILayout.Height(alto - 130f)
            );

            try
            {
                GUILayout.Label(
                    "OBJETIVO\n" +
                    "Controla el bot, recoge comida, alimenta al slime del corral, " +
                    "captura slimes salvajes y produce orbes para venderlos.\n\n" +

                    "MOVIMIENTO\n" +
                    "mover_derecha(): mueve el bot a la derecha.\n" +
                    "mover_izquierda(): mueve el bot a la izquierda.\n" +
                    "mover_arriba(): mueve el bot hacia arriba.\n" +
                    "mover_abajo(): mueve el bot hacia abajo.\n\n" +

                    "COMIDA Y SLIMES\n" +
                    "recoger(): recoge comida en la casilla del bot.\n" +
                    "alimentar(): entrega comida al slime del corral.\n" +
                    "capturar(): intenta capturar un slime salvaje en su casilla.\n\n" +

                    "OR​​BES Y DINERO\n" +
                    "vender_orbes(): vende los orbes disponibles.\n" +
                    "dinero(): consulta el dinero disponible.\n" +
                    "orbes(): consulta la cantidad de orbes.\n" +
                    "llaves(): consulta las llaves disponibles.\n" +
                    "imprimir(expresion): muestra información en la salida.\n\n" +

                    "SENSORES\n" +
                    "hay_comida(): comprueba si hay comida.\n" +
                    "hay_comida_aqui(): comprueba si hay comida en la posición actual.\n" +
                    "comida_x() y comida_y(): consultan coordenadas de comida.\n" +
                    "hay_slime_salvaje(): comprueba si hay un slime salvaje.\n" +
                    "slime_x() y slime_y(): consultan sus coordenadas.\n" +
                    "distancia_slime(): consulta la distancia al slime.\n" +
                    "energia_slime(): consulta su energía.\n" +
                    "hay_orbes(): comprueba si hay orbes disponibles.\n\n" +

                    "PROGRAMACIÓN\n" +
                    "Usa las variables, condiciones, ciclos y funciones que admite " +
                    "el editor del bot. Escribe los comandos con la sintaxis correcta " +
                    "y revisa la sección Salida para conocer el resultado.",
                    texto
                );
            }
            finally
            {
                GUILayout.EndScrollView();
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("CERRAR", boton, GUILayout.Height(45f)))
            {
                mostrarControles = false;
            }
        }
        finally
        {
            GUILayout.EndArea();
        }
    }





    private void DibujarVentanaIdiomas()
    {
        float ancho = Mathf.Min(500f, Screen.width * 0.85f);
        float alto = 300f;

        float x = (Screen.width - ancho) / 2f;
        float y = (Screen.height - alto) / 2f;

        Rect ventana = new Rect(x, y, ancho, alto);

        GUI.color = new Color(0.08f, 0.10f, 0.16f, 1f);
        GUI.DrawTexture(ventana, Texture2D.whiteTexture);
        GUI.color = Color.white;

        GUIStyle titulo = new GUIStyle(GUI.skin.label);
        titulo.fontSize = 24;
        titulo.fontStyle = FontStyle.Bold;
        titulo.alignment = TextAnchor.MiddleCenter;
        titulo.normal.textColor = Color.yellow;

        GUI.Label(
            new Rect(x + 15, y + 15, ancho - 30, 45),
            "SELECCIONAR IDIOMA",
            titulo
        );

        GUIStyle boton = new GUIStyle(GUI.skin.button);
        boton.fontSize = 18;
        boton.fontStyle = FontStyle.Bold;

        if (GUI.Button(
            new Rect(x + 60, y + 85, ancho - 120, 45),
            "ESPAÑOL",
            boton))
        {
            idiomaSeleccionado = "Español";
            Debug.Log("Idioma seleccionado: " + idiomaSeleccionado);
        }

        if (GUI.Button(
            new Rect(x + 60, y + 145, ancho - 120, 45),
            "ENGLISH",
            boton))
        {
            idiomaSeleccionado = "English";
            Debug.Log("Selected language: " + idiomaSeleccionado);
        }

        if (GUI.Button(
            new Rect(x + 150, y + 225, ancho - 300, 40),
            "CERRAR",
            boton))
        {
            mostrarIdiomas = false;
        }
    }



}
