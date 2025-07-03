window.addEventListener("DOMContentLoaded", async () => {
    const userId = parseInt(localStorage.getItem("userId"));
    const userRol = parseInt(localStorage.getItem("userRoleId"));
    let stepId = null;
    let proyectoId = null;

    if (!userId || !userRol) {
        alert("Error: No se pudo obtener la información del usuario.");
        window.location.href = "login.html";
        return;
    }

    try {
        const pendientesRes = await fetch(`https://localhost:7252/api/Project/pendientes/${userId}`);
        if (!pendientesRes.ok) {
            const error = await pendientesRes.json();
            alert("Error al cargar los proyectos pendientes: " + error.message);
            return;
        }

        const proyectosPendientes = await pendientesRes.json();
        if (proyectosPendientes.length === 0) {
            alert("No tienes proyectos pendientes de aprobación.");
            window.location.href = "index.html";
            return;
        }

        const primerProyecto = proyectosPendientes[0];
        proyectoId = primerProyecto.projectId;
        await cargarProyecto(proyectoId, userRol);

    } catch (error) {
        console.error("Error al cargar los proyectos pendientes:", error);
        alert("Error al cargar los proyectos pendientes.");
    }

    async function cargarProyecto(id, userRol) {
    try {
        const res = await fetch(`https://localhost:7252/api/Project/${id}`, {
            method: "GET",
            headers: {
            "Content-Type": "application/json"
            }
            });
        if (!res.ok) {
            const error = await res.json();
            alert("Error al cargar el proyecto: " + error.message);
            return;
        }

        const data = await res.json();

        console.log("Estado del proyecto:", data.status?.name);
        if (data.status?.name?.toLowerCase() !== "pending") {
            alert("El proyecto no se encuentra en estado pendiente.");
            window.location.href = "index.html";
            return;
        }

        document.getElementById("titulo").value = data.title;
        document.getElementById("descripcion").value = data.description;
        document.getElementById("monto").value = data.amount;
        document.getElementById("duracion").value = data.duration;
        document.getElementById("area").value = data.area?.name || "No especificado";
        document.getElementById("tipo").value = data.type?.name || "No especificado";

        const pasoDelUsuario = data.steps.find(
            (p) =>
                parseInt(p.approverRole?.id) === userRol &&
                p.status?.name?.toLowerCase() === "pending"
        );

        if (!pasoDelUsuario) {
            alert("No hay pasos pendientes para este usuario en este proyecto.");
            return;
        }

        stepId = pasoDelUsuario.id;

    } catch (error) {
        console.error("Error al cargar el proyecto:", error);
        alert("Error al cargar el proyecto.");
    }
}

    async function enviarDecision(statusId) {

        const data = {
            id: parseInt(stepId),
            user: userId,
            status: parseInt(statusId),
            observation: document.getElementById("observacion").value ?? ""
        };

        try {
            const response = await fetch(`https://localhost:7252/api/Project/${proyectoId}/decision`, {
            method: "PATCH",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data),
            });

            console.log("Respuesta cruda:", response);

            const resultText = await response.text();
            console.log("Texto de respuesta:", resultText);

            if (!response.ok) {
            try {
                const error = await response.json();
                alert("Error: " + (error.message || "Error desconocido"));
            } catch {
                const fallback = await response.text();
                alert("Error: " + fallback);
            }
            return;
        }

            alert("Decisión aplicada con éxito.");

            const pendientes = await fetch(`https://localhost:7252/api/Project/pendientes/${userId}`);
            if (!pendientes.ok) {
                window.location.href = "index.html";
                return;
            }

            const lista = await pendientes.json();
            const siguiente = lista.find((p) => p.status?.name?.toLowerCase() === "pending");

            if (siguiente && siguiente.projectId) {
                window.location.href = `Approver.html?id=${siguiente.projectId}`;
            } else {
                window.location.href = "index.html";
            }
        } catch (error) {
            console.error("Excepción atrapada:", error);
            alert("Error al enviar decisión: " + error.message);
        }
    }

    document.getElementById("btnAprobar").addEventListener("click", () => enviarDecision(2));
    document.getElementById("btnRechazar").addEventListener("click", () => enviarDecision(3));
    document.getElementById("btnObservar").addEventListener("click", () => enviarDecision(4));
});