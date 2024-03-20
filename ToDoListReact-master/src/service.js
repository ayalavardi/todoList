import axios from 'axios';

const apiUrl = "http://localhost:5290/todoitems"

export default {
  getTasks: async () => {
    const result = await axios.get(`${apiUrl}`)  
    console.log(result.data);  
    return result.data;
    
  },

  addTask: async(name)=>{
    const result = await axios.post(`${apiUrl}/${name}`)  
    console.log('addTask', name)
    //TODO
    return {};
  },

  setCompleted: async(id, isComplete)=>{
    const result = await axios.put(`${apiUrl}/${id}/${isComplete}`)  

    console.log('setCompleted', {id, isComplete})
    //TODO
    return {};
  },

  deleteTask:async(id)=>{
    const result = await axios.delete(`${apiUrl}/${id}`)  
    console.log(result.data);  
    return result.data;  }
};
