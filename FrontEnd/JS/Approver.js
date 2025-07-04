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
        const proyectosRes = await fetch(`https://localhost:7252/api/Project?status=1&approvalUser=${userId}`);
        if (!proyectosRes.ok) {
            const error = await proyectosRes.json();
            alert("Error al cargar los proyectos: " + error.message);
            return;
        }

        const proyectos = await proyectosRes.json();
        if (proyectos.length === 0) {
            alert("No tienes proyectos pendientes de aprobación.");
            window.location.href = "index.html";
            return;
        }

        const primerProyecto = proyectos[0];
        proyectoId = primerProyecto.id;
        await cargarProyectoCompleto(proyectoId, userRol);

    } catch (error) {
        console.error("Error al cargar proyectos:", error);
        alert("Error al cargar proyectos pendientes.");
    }

    async function cargarProyectoCompleto(id, userRol) {
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

            if (data.status?.name?.toLowerCase() !== "pending") {
                alert("El proyecto ya no está en estado pendiente.");
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
                (step) =>
                    parseInt(step.approverRole?.id) === userRol &&
                    step.status?.name?.toLowerCase() === "pending"
            );

            if (!pasoDelUsuario) {
                alert("No hay pasos pendientes para tu rol en este proyecto.");
                window.location.href = "index.html";
                return;
            }

            stepId = pasoDelUsuario.id;

        } catch (error) {
            console.error("Error al cargar proyecto completo:", error);
            alert("Error al cargar proyecto.");
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

            // Cargar siguiente proyecto pendiente
            const nuevosRes = await fetch(`https://localhost:7252/api/Project?status=1&approvalUser=${userId}`);
            const nuevos = await nuevosRes.json();
            const siguiente = nuevos.find(p => p.id !== proyectoId); // evitar repetir

            if (siguiente) {
                window.location.href = `Approver.html?id=${siguiente.id}`;
            } else {
                window.location.href = "index.html";
            }

        } catch (error) {
            console.error("Excepción al enviar decisión:", error);
            alert("Error al enviar decisión.");
        }
    }

    document.getElementById("btnAprobar").addEventListener("click", () => enviarDecision(2));
    document.getElementById("btnRechazar").addEventListener("click", () => enviarDecision(3));
    document.getElementById("btnObservar").addEventListener("click", () => enviarDecision(4));
});