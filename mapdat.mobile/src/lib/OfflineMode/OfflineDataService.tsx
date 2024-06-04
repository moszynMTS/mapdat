import * as FileSystem from 'expo-file-system';

class OfflineDataService {
  path: string;
  constructor() {
    this.path = `${FileSystem.documentDirectory}/`;
  }

  async getData(name: string) {
    return await this.loadMapData(name);
  }

  /* HANDLE FILE I/O */
  async saveMapData(data: string, name: string) {
    try {
      const path = `${this.path}${name}.json`;
      await FileSystem.writeAsStringAsync(path, JSON.stringify(data), { encoding: 'utf8' });
      console.debug("SAVED");
    } catch (error) {
      console.error("Error saving mapData:", error);
    }
  }

  async deleteMapData(name: string) {
    const folderPath = `${this.path}${name}.json`;
    try {
      await FileSystem.deleteAsync(folderPath);
      console.log("Folder został pomyślnie usunięty.");
    } catch (error) {
      console.error("Błąd podczas usuwania folderu:", error);
    }
  }

  async listJsonFiles() {
    try {

      const files = await FileSystem.readDirectoryAsync(this.path);
      const jsonFiles = files.filter(file => file.endsWith('.json')); // Filter for files ending with '.json'
      return jsonFiles; // Return an array of JSON file names (without paths)
    } catch (error) {
      console.error("Error listing JSON files:", error);
      return []; // Return an empty array on error
    }
  }

  async loadMapData(name: string) {
    try {
      const path = `${this.path}${name}.json`;
      const fileExists = await FileSystem.getInfoAsync(path).then(({ exists }) => exists);
      if (fileExists) {
        const storedData = await FileSystem.readAsStringAsync(path, { encoding: 'utf8' });
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
      const path = `${this.path}/${name}.json`;
      await FileSystem.writeAsStringAsync(path, "{}", { encoding: 'utf8' });
      console.info("Cleared: " + name + " succes");
    } catch (error) {
      console.error("Error delete taskQueue:", error);
    }
  }
}

export default new OfflineDataService();