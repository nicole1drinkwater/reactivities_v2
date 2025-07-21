import { useQuery } from "@tanstack/react-query";
import axios from "axios";

export const useActivities = () => {
    const {data: activities, isPending} = useQuery({
    queryKey: ['activities'],
    queryFn: async () => {
    try {
      const response = await axios.get<Activity[]>("http://localhost:5001/api/activities");
      return response.data;
    } catch (err) {
      console.error("Error fetching activities", err);
      throw err;
    }
}

  });

  return {
    activities,
    isPending
  }
}