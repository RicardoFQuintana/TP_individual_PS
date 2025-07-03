document.addEventListener("DOMContentLoaded", function () {
  const userId = parseInt(localStorage.getItem("userId"));
  const userName = localStorage.getItem("userName");

  // Redireccionar si no hay usuario logueado
  if (!userId) {
    alert("No iniciaste sesión. Redirigiendo al login...");
    window.location.href = "login.html";
    return;
  }

  // Mostrar nombre del usuario en el campo
  document.getElementById("nombreUsuario").value = userName;

  // ===================== CARGAR ÁREAS =====================
  const areaSelect = document.getElementById("AreaSelect");

  fetch("https://localhost:7252/api/Area")
    .then(response => {
      if (!response.ok) throw new Error("Error al cargar áreas");
      return response.json();
    })
    .then(areas => {
      areas.forEach(area => {
        const option = document.createElement("option");
        option.value = area.id;
        option.textContent = area.name;
        areaSelect.appendChild(option);
      });
    })
    .catch(error => {
      console.error("Error al obtener las áreas:", error);
    });

  // ===================== CARGAR TIPOS =====================
  const tipoSelect = document.getElementById("tipoSelect");

  fetch("https://localhost:7252/api/ProjectType")
    .then(response => {
      if (!response.ok) throw new Error("Error al cargar tipos");
      return response.json();
    })
    .then(tipos => {
      tipos.forEach(tipo => {
        const option = document.createElement("option");
        option.value = tipo.id;
        option.textContent = tipo.name;
        tipoSelect.appendChild(option);
      });
    })
    .catch(error => {
      console.error("Error al obtener tipos:", error);
    });

  // ===================== ENVÍO DEL FORMULARIO =====================
  const form = document.getElementById("formProyecto");

  form.addEventListener("submit", function (e) {
    e.preventDefault();

    const data = {
      title: document.getElementById("titulo").value,
      description: document.getElementById("descripcion").value,
      amount: parseFloat(document.getElementById("monto").value),
      duration: parseInt(document.getElementById("duracion").value),
      area: parseInt(areaSelect.value),
      user: userId,
      type: parseInt(tipoSelect.value)
      
    };

    fetch("https://localhost:7252/api/Project", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    })
      .then(response => {
        if (!response.ok) throw new Error("No se pudo crear la propuesta");
        return response.json();
      })
      .then(res => {
        alert("Propuesta creada con éxito.");
        form.reset(); // Limpiar el formulario
        window.location.href = `ProjectID.html?id=${res.id}`;
      })
      .catch(error => {
        alert("Error al crear la propuesta: " + error.message);
        console.error(error);
      });
  });
});