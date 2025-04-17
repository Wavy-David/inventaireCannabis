package com.example.toto;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.recyclerview.widget.RecyclerView;
import java.util.List;

public class HistoriqueAdapter extends RecyclerView.Adapter<HistoriqueAdapter.ViewHolder> {
    private List<HistoriqueModel> historiqueList;

    public HistoriqueAdapter(List<HistoriqueModel> historiqueList) {
        this.historiqueList = historiqueList;
    }

    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item_historique, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(ViewHolder holder, int position) {
        HistoriqueModel item = historiqueList.get(position);
        holder.txtAction.setText("Action: " + item.action);
        holder.txtDate.setText("Date: " + item.date);
        holder.txtChamp.setText("Champ: " + item.champ);
        holder.txtAncienne.setText("Ancienne: " + item.ancienneValeur);
        holder.txtNouvelle.setText("Nouvelle: " + item.nouvelleValeur);
    }

    @Override
    public int getItemCount() {
        return historiqueList.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder {
        TextView txtAction, txtDate, txtChamp, txtAncienne, txtNouvelle;

        public ViewHolder(View itemView) {
            super(itemView);
            txtAction = itemView.findViewById(R.id.txtAction);
            txtDate = itemView.findViewById(R.id.txtDate);
            txtChamp = itemView.findViewById(R.id.txtChamp);
            txtAncienne = itemView.findViewById(R.id.txtAncienne);
            txtNouvelle = itemView.findViewById(R.id.txtNouvelle);
        }
    }
}