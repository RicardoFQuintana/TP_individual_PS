window.addEventListener("DOMContentLoaded", () => {
  const userId = parseInt(localStorage.getItem("userId"));
  const userName = localStorage.getItem("userName");

  // Redireccionar si no hay usuario logueado
  if (!userId) {
    alert("No iniciaste sesión. Redirigiendo al login...");
    window.location.href = "login.html";
    return;
  }

  // Mostrar nombre del usuario en el campo
  document.getElementById("nombreUsuario").textContent = userName;

  // Elementos de filtro
  const tituloInput = document.getElementById("filtroTitulo");
  const estadoSelect = document.getElementById("filtroEstado");
  const tipoSelect = document.getElementById("filtroTipo");
  const areaSelect = document.getElementById("filtroArea");

  // Contenedor de propuestas
  const seccionPropuestas = document.querySelector("section");

  // Cargar combos dinámicos
  cargarFiltros();  

  // Llamada inicial
  cargarPropuestas();

  // Listeners de cambio
  tituloInput.addEventListener("input", cargarPropuestas);
  estadoSelect.addEventListener("change", cargarPropuestas);
  tipoSelect.addEventListener("change", cargarPropuestas);
  areaSelect.addEventListener("change", cargarPropuestas);

  async function cargarFiltros() {
    await cargarOpciones("https://localhost:7252/api/Area", "filtroArea");
    await cargarOpciones("https://localhost:7252/api/ProjectType", "filtroTipo");
    await cargarOpciones("https://localhost:7252/api/ApprovalStatus", "filtroEstado");
  }

  async function cargarOpciones(url, selectId) {
    try {
      const response = await fetch(url);
      if (!response.ok) throw new Error("Error al cargar opciones");

      const data = await response.json();
      const select = document.getElementById(selectId);

      // Limpiar select y dejar opción por defecto
      select.innerHTML = `<option value="">Todos</option>`;

      data.forEach(item => {
        const option = document.createElement("option");
        option.value = item.id;
        option.textContent = item.name;
        select.appendChild(option);
      });
    } catch (error) {
      console.error(`Error al cargar ${selectId}:`, error);
    }
  }

  async function cargarPropuestas() {
    seccionPropuestas.innerHTML = "Cargando...";

    const title = tituloInput.value.trim();
    const statusId = estadoSelect.value || null;
    const typeId = tipoSelect.value || null;
    const areaId = areaSelect.value || null;
    const applicant = userId;

    const queryParams = new URLSearchParams();
    if (title) queryParams.append("title", title);
    if (statusId) queryParams.append("status", statusId);
    if (typeId) queryParams.append("typeId", typeId);
    if (areaId) queryParams.append("areaId", areaId);
    if (applicant) queryParams.append("applicant", applicant);

    try {
      const response = await fetch(`https://localhost:7252/api/Project?${queryParams.toString()}`);

      if (!response.ok) {
        const error = await response.json();
        seccionPropuestas.innerHTML = `<p style='color:red;'>${error.message}</p>`;
        return;
      }

      const proyectos = await response.json();
      renderizarPropuestas(proyectos);

    } catch (error) {
      console.error("Error al cargar propuestas:", error);
      seccionPropuestas.innerHTML = "<p style='color:red;'>Error al cargar propuestas.</p>";
    }
  }

  function renderizarPropuestas(proyectos) {
    seccionPropuestas.innerHTML = "";

    if (proyectos.length === 0) {
      seccionPropuestas.innerHTML = "<p>No se encontraron propuestas.</p>";
      return;
    }

    proyectos.forEach(p => {
      const card = document.createElement("div");
      card.className = "propuesta-card";

      const titulo = document.createElement("h4");
      titulo.textContent = p.title;

      const estado = document.createElement("p");
      estado.className = "estado";
      estado.textContent = p.status;

      const botones = document.createElement("div");
      botones.className = "card-buttons";

      const editar = document.createElement("button");
      editar.textContent = "✏ Editar";
      editar.onclick = () => location.href = `edit.html?id=${p.id}`;

      const detallar = document.createElement("button");
      detallar.textContent = "🔍 Detallar";
      detallar.onclick = () => location.href = `ProjectID.html?id=${p.id}`;

      botones.appendChild(editar);
      botones.appendChild(detallar);

      card.appendChild(titulo);
      card.appendChild(estado);
      card.appendChild(botones);

      seccionPropuestas.appendChild(card);
    });
  }
});
