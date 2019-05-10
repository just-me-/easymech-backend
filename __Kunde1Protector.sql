
--Procedure
--Man könnte auch einfach return null statt expection, dann passiert nix, oder man könnte noch spezifisch prüfen ob der Name / IstAktiv geändert wird und anderes Zeug zulassen.
--Procedure änderbar ohne Trigger neu aufzusetzen.
create or replace function cust1Protector() returns trigger as $$
begin
	if (OLD."Id" = 1::bigint)
	then raise exception 'Kunde 1 kann nicht editiert oder gelöscht werden';
	end if;
	return  NEW;
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