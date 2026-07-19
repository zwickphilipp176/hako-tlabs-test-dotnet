import { defineStore } from "pinia";

export const useToDoStore = defineStore("toDoStore", {
  state: () => ({
    tasks: [],
    searchTerm: "",
  }),
  actions: {
    async fetchTasks() {
      // get tasks from localhost:5000/api/tasks
      const searchExpression = this.searchTerm?.length > 0 ? `Filter.SearchTerm=${this.searchTerm}` : "";
      const response = await fetch(`http://localhost:5000/api/tasks?${searchExpression}`);
      const data = await response.json();
      this.tasks = data.items;
    },
    async addTask(task) {
      await fetch("http://localhost:5000/api/tasks", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(task),
      }).finally(() => {
        this.searchTerm = "";
        this.fetchTasks();
      });
    },

    async editTask(task) {
      await fetch(`http://localhost:5000/api/tasks/${task.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(task),
      }).finally(() => {
        this.searchTerm = "";
        this.fetchTasks();
      });
    },

    async deleteTask(task) {
      await fetch(`http://localhost:5000/api/tasks/${task.id}`, {
        method: "DELETE",
      }).finally(() => {
        this.searchTerm = "";
        this.fetchTasks();
      });
    },
  },
});
