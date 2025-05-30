const { defineConfig } = require("cypress");

module.exports = defineConfig({
  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
      this.screenshotOnRunFailure = true;
      this.video = true;
    },
    baseUrl: "https://booksaw.huynhthanhson.io.vn/",
    // baseUrl: "http://localhost:62416",
  },
});
