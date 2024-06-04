import RNFS from "react-native-fs";

class OfflineDataService {
  constructor() {}

  async getData(name: string) {
    return await this.loadMapData(name);
  }

  /* HANDLE FILE I/O */

  async saveMapData(data: string, name: string) {
    try {
      const path = RNFS.DocumentDirectoryPath + "/" + name + ".json";
      await RNFS.writeFile(path, JSON.stringify(data), "utf8");
    } catch (error) {
      console.error("Error saving mapData:", error);
    }
  }

  async deleteMapData(name: string) {
    const folderPath = RNFS.DocumentDirectoryPath + "/" + name + ".json";
    RNFS.unlink(folderPath)
      .then(() => {
        console.log("Folder został pomyślnie usunięty.");
      })
      .catch((error) => {
        console.error("Błąd podczas usuwania folderu:", error);
      });
  }

  async loadMapData(name: string) {
    try {
      const path = RNFS.DocumentDirectoryPath + "/" + name + ".json";

      const fileExists = await RNFS.exists(path);

      if (fileExists) {
        const storedData = await RNFS.readFile(path, "utf8");

        if (storedData) {
          return JSON.parse(storedData);
        }
      } else {
        console.warn("File not found: " + name + ".json");
      }
    } catch (error) {
      console.error("Error loading data:", error);
    }
  }
  async clearMapData(name: string) {
    try {
      const path = RNFS.DocumentDirectoryPath + "/" + name + ".json";
      await RNFS.writeFile(path, "{}", "utf8");
      console.info("Cleared: " + name + " succes");
    } catch (error) {
      console.error("Error delete taskQueue:", error);
    }
  }
}

export default new OfflineDataService();
