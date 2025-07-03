document.addEventListener("DOMContentLoaded", function () {
  const select = document.getElementById("usuarioSelect");

  let usuariosCargados = [];

  fetch("https://localhost:7252/api/User")
    .then(response => {
      if (!response.ok) throw new Error("Error al cargar usuarios");
      return response.json();
    })
    .then(usuarios => {
      usuariosCargados = usuarios;
      usuarios.forEach(usuario => {
        const option = document.createElement("option");
        option.value = usuario.id;
        option.textContent =  `${usuario.name} (${usuario.role.name})`;
        option.dataset.roleId = usuario.role.id;
        select.appendChild(option);
      });
    })
    .catch(error => {
      console.error("Error al obtener los usuarios:", error);
      const errorOption = document.createElement("option");
      errorOption.textContent = "Error al cargar usuarios";
      errorOption.disabled = true;
      select.appendChild(errorOption);
    });
});

document.querySelector("form").addEventListener("submit", function (e) {
  e.preventDefault();

  const select = document.getElementById("usuarioSelect");
  const userId = select.value;

  if (!userId) {
    alert("Por favor seleccioná un usuario para ingresar.");
    return;
  }


  // Guardar datos en localStorage
  localStorage.setItem("userId", userId);

  const selectedOption = select.options[select.selectedIndex];
  localStorage.setItem("userName", selectedOption.text);
  localStorage.setItem("userRoleId", selectedOption.dataset.roleId);

  

  // Redirigir a la siguiente pantalla
  window.location.href = "index.html";
});