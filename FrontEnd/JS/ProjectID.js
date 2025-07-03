window.addEventListener('DOMContentLoaded', async () => {
    const params = new URLSearchParams(window.location.search);
    const id = params.get("id");
    const userName = localStorage.getItem("userName");

    if (!id) {
        alert("Proyecto no especificado");
        return;
    }

    try {
        const res = await fetch(`https://localhost:7252/api/Project/${id}`);
        if (!res.ok) {
            const error = await res.json();
            alert("Error: " + error.message);
            return;
        }

        const data = await res.json();

        document.getElementById("titulo").value = data.title;
        document.getElementById("descripcion").value = data.description;
        document.getElementById("monto").value = data.amount;
        document.getElementById("duracion").value = data.duration;
        document.getElementById("area").value = data.area?.name || 'No especificado';
        document.getElementById("tipo").value = data.type?.name || 'No especificado';
        document.getElementById("estado").value = data.status?.name || 'No especificado';
        document.getElementById("nombreUsuario").value = userName;

        const pasosList = document.getElementById("pasos");
        pasosList.innerHTML = "";

        let observacionesTexto = "";

        data.steps.forEach((paso, index) => {
        const stepBox = document.createElement("div");
        stepBox.classList.add("step-box");

        const circle = document.createElement("div");
        circle.classList.add("step-circle");

        const status = paso.status?.name?.toLowerCase();
        if (status === "approved") {
            circle.textContent = "✓";
            circle.classList.add("step-approved");
        } else if (status === "rejected") {
            circle.textContent = "✖";
            circle.classList.add("step-rejected");
        } else if (status === "observed") {
            circle.textContent = "👁";
            circle.classList.add("step-observed");
        }else if (status === "pending") {
                circle.textContent = "⏳";
                circle.classList.add("step-pending");
        }else {
            circle.textContent = "?";
        }

        const info = document.createElement("div");
        info.classList.add("step-info");
        info.innerHTML = `<strong>${paso.status?.name || "?"}</strong><br>${paso.approverRole?.name || "Rol"}<br>${paso.approver?.name || "Usuario"}`;

        if (paso.observations) {
            observacionesTexto += `Por: ${paso.approver?.name ?? "Usuario"} (${paso.approverRole?.name ?? "Rol"})\n`;
            observacionesTexto += `Observación: ${paso.observations}\n`;
        }

        stepBox.appendChild(circle);
        stepBox.appendChild(info);
        pasosList.appendChild(stepBox);

        // Si no es el último paso, agregar flecha
        if (index < data.steps.length - 1) {
            const arrow = document.createElement("div");
            arrow.classList.add("arrow");
            arrow.textContent = "→";
            pasosList.appendChild(arrow);
        }
        });

        if (observacionesTexto.trim() !== "") {
            document.getElementById("observacion").value = observacionesTexto.trim();
            document.getElementById("observacionBlock").style.display = "block";
        }

    } catch (error) {
        console.error(error);
        alert("Error al cargar el proyecto");
    }
});