import * as FileSystem from 'expo-file-system';
import { removeElementByName } from '../../utils/removeElementByName';

class OfflineDataService {
  path: string;
  constructor() {
    this.path = `${FileSystem.documentDirectory}`;
    this.createMainFile();
  }

  async createMainFile(){
    FileSystem.getInfoAsync(`${FileSystem.documentDirectory}/layers.json`).then(async tmp => {
      if(!tmp.exists)
      {
        await FileSystem.writeAsStringAsync(`${this.path}layers.json`, JSON.stringify({layersName: []}), { encoding: 'utf8' });
      }
    })
  }

  async getData(name: string) {
    return await this.loadMapData(name);
  }

  /* HANDLE FILE I/O */
  async saveMapName(name:string)
  {
    try {
      const path = `${this.path}layers.json`;
      const storedData = await FileSystem.readAsStringAsync(path, { encoding: 'utf8' });
      const parsedData = JSON.parse(storedData);
      parsedData.layersName.push(name);
      await FileSystem.writeAsStringAsync(path, JSON.stringify(parsedData), { encoding: 'utf8' });
    } catch (error) {
      console.error("Error saving mapData:", error);
    }
  }

  async saveMapData(data: string, name: string, areaInfo: unknown) {
    try {
      this.saveMapName(name);
      const path = `${this.path}${name}.json`;
      const areaInfopath = `${this.path}${name}_data.json`;
      await FileSystem.writeAsStringAsync(path, JSON.stringify(data), { encoding: 'utf8' });
      await FileSystem.writeAsStringAsync(areaInfopath, JSON.stringify(areaInfo), { encoding: 'utf8' });
      console.debug("SAVED");
    } catch (error) {
      console.error("Error saving mapData:", error);
    }
  }



  async listJsonFiles(): Promise<{ layersName: []; }> {
    try { 
      console.log("LIST");
      const path = `${this.path}layers.json`;
      const storedData = await FileSystem.readAsStringAsync(path, { encoding: 'utf8' });
      return JSON.parse(storedData); 
    } catch (error) {
      console.error("Error listing JSON files:", error);
      return {layersName: []}; // Return an empty array on error
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
  async loadAreaInfoData(name: string) {
    try {
      const path = `${this.path}${name}_data.json`;
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

  private async deleteMapData(name: string) {
    const folderPath = `${this.path}${name}.json`;
    try {
      await FileSystem.deleteAsync(folderPath);
      console.log("Folder został pomyślnie usunięty.");
    } catch (error) {
      console.error("Błąd podczas usuwania folderu:", error);
    }
  }
  private async deleteAreaInfoData(name: string){
    const folderPath = `${this.path}${name}_data.json`;
    try {
      await FileSystem.deleteAsync(folderPath);
      console.log("Folder został pomyślnie usunięty.");
    } catch (error) {
      console.error("Błąd podczas usuwania folderu:", error);
    }
  }

  async deleteMap(name:string){
    try {
      const path = `${this.path}layers.json`;
      console.log(path)
      const storedData = await FileSystem.readAsStringAsync(path, { encoding: 'utf8' });
      const parsedData = JSON.parse(storedData);
      parsedData.layersName = removeElementByName(parsedData.layersName, name);
      await FileSystem.writeAsStringAsync(path, JSON.stringify(parsedData), { encoding: 'utf8' });
      await this.deleteMapData(name);
      await this.deleteAreaInfoData(name);
    } catch (error) {
      console.error("Error deleting mapData:", error);
    }

  }
}

export default new OfflineDataService();