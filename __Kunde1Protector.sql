
--Procedure
create or replace function cust1Protector() returns trigger as $$
begin
	if (TG_OP = 'UPDATE') then
		if (OLD."Id" = 1 and NEW."Firma" != OLD."Firma")
			then raise exception 'Kunde 1 kann nicht umbenennt werden';
		end if;
	
		if (OLD."Id" = 1 and not NEW."IstAktiv")
			then raise exception 'Kunde 1 kann nicht inaktiv gesetzt werden';
		end if;
		
		return NEW;
	end if;
	
	if (TG_OP = 'DELETE') then
		if (OLD."Id" = 1)
			then raise exception 'Kunde 1 kann nicht aus der Datenbank gelöscht werden';
		end if;
		
		return OLD;
	end if;
end;
$$ language plpgsql;



--Trigger
--Note: Truncate ist zugelassen; sollte man eig auch hinzufügen aber wegen selenium etc nicht drin.
create trigger cust1Protector before update or delete on "Kunden"
for each row
execute procedure cust1Protector();

--Temporär abschalten bzw einschalten:
ALTER TABLE "Kunden" DISABLE TRIGGER USER
ALTER TABLE "Kunden" Enable TRIGGER USER